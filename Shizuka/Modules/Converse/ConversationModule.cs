using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Shizuka.Models;

namespace Shizuka.Modules.Converse
{
	public class ConversationModule : Module
	{
		private List<ResponseMap> responseMaps;
		private List<AmityMap> userAmity;

		private List<WordModel> wordValance;

		private List<SentimentResult> results;

		private string session = Guid.NewGuid().ToString();
		private string dataDir;

		public ConversationModule() : base("Conversation Module", "Allows Shizuka to coverse with users using Neural Networks to interpret and respond to messages.")
		{
			responseMaps = new List<ResponseMap>();
			results = new List<SentimentResult>();
		}

		public override void Init(Server server)
		{
			dataDir = $"{server.DataDir}/{Name}";
			if (!Directory.Exists(dataDir))
				Directory.CreateDirectory(dataDir);
			server.ShizukaMentioned += ShizukaConverse;
			server.MessageReceived += e =>
			{
				Console.WriteLine($"Sentiment: {EvaluateSentiment(e.Content)} -> \"{e.Content}\"");
			};

			if (File.Exists($"{dataDir}/userAmity.json"))
				userAmity = JsonConvert.DeserializeObject<List<AmityMap>>(File.ReadAllText($"{dataDir}/userAmity.json"));
			else
			{
				userAmity = new List<AmityMap>();
				File.WriteAllText($"{dataDir}/userAmity.json", JsonConvert.SerializeObject(userAmity));
			}

			if (File.Exists($"{dataDir}/words.large.json"))
				wordValance = JsonConvert.DeserializeObject<List<WordModel>>(File.ReadAllText($"{dataDir}/words.large.json"));
			else
				wordValance = new List<WordModel>();

			responseMaps = new List<ResponseMap>
			{
				new ResponseMap(new string[]
				{
					"Hello",
					"Heyo",
					"Hi!",
				}, m =>
				{

				})
			};
		}

		public Embed AnalyzeData(string file)
		{
			var sr = JsonConvert.DeserializeObject<List<SentimentResult>>(File.ReadAllText(file));
			float totalS = 0;
			Dictionary<string, int> keywordCount = new Dictionary<string, int>();
			foreach (SentimentResult s in sr)
			{
				totalS += s.Sentiment;
				foreach (string k in s.Keywords)
				{
					if (keywordCount.ContainsKey(k))
						keywordCount[k] += 1;
					else
						keywordCount.Add(k, 1);
				}
			}
			EmbedBuilder output = new EmbedBuilder();
			output.AddField("The total sentiment", totalS);
			output.AddField(f =>
			{
				f.Name = "Top Keywords";
				foreach (KeyValuePair<string, int> k in (from KeyValuePair<string, int> kw in keywordCount orderby kw.Value descending select kw).Take(10))
				{
					f.Value += $"**{k.Key}**: {k.Value} uses \t `{wordValance.First(x=> x.Word == k.Key).Valence}`\n";
				}
			});
			return output.Build();
		}


		public float EvaluateSentiment(string input)
		{
			float baseAmity = 0;
			string[] words = input.ToLower().Split(' ');
			float totalValance = 0;
			for (int i = 0; i < words.Length; i++)
			{
				int? v = wordValance.FirstOrDefault(x => x.Word == words[i])?.Valence;
				totalValance += (v == null) ? 0 : (int)v;
			}
			baseAmity += totalValance / words.Length;
			results.Add(new SentimentResult(input, baseAmity, (from WordModel s in wordValance where words.Any(x => x == s.Word) select s.Word).ToArray()));
			File.WriteAllText($"{dataDir}/results-{session}.json", JsonConvert.SerializeObject(results));
			return baseAmity;
		}

		private void ShizukaConverse(SocketUserMessage e)
		{
			Console.WriteLine($"Sentiment: {EvaluateSentiment(e.Content)} -> \"{e.Content}\"");
			string message = e.TextWithoutMention();
			SocketUser user = e.Author;
			SocketGuildChannel channel = e.Channel as SocketGuildChannel;
		}

		public override Task Respond(SocketUserMessage message)
		{
			
			return Task.CompletedTask;
		}

		public override ModuleModel GetModel()
		{
			throw new NotImplementedException();
		}
	}
}
