using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Shizuka.Modules.Converse
{
	public class ConversationModule : Module
	{
		private List<ResponseMap> responseMaps;
		private List<AmityMap> userAmity;

		private List<WordModel> wordValance;

		private List<SentimentResult> results;

		private string session = Guid.NewGuid().ToString();

		public ConversationModule() : base("Conversation Module", "Allows Shizuka to coverse with users using Neural Networks to interpret and respond to messages.")
		{
			responseMaps = new List<ResponseMap>();
			results = new List<SentimentResult>();
		}

		public override void Init(Server server)
		{
			server.ShizukaMentioned += ShizukaConverse;
			server.MessageReceived += e =>
			{
				Console.WriteLine($"Sentiment: {EvaluateSentiment(e.Content)} -> \"{e.Content}\"");
			};
			if (!Directory.Exists("Converse/"))
				Directory.CreateDirectory("Converse/");

			if (File.Exists("Converse/userAmity.json"))
				userAmity = JsonConvert.DeserializeObject<List<AmityMap>>(File.ReadAllText("Converse/userAmity.json"));
			else
			{
				userAmity = new List<AmityMap>();
				File.WriteAllText("Converse/userAmity.json", JsonConvert.SerializeObject(userAmity));
			}

			if (File.Exists("Converse/words.large.json"))
				wordValance = JsonConvert.DeserializeObject<List<WordModel>>(File.ReadAllText("Converse/words.large.json"));
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

		public static void LoadRawData(string path, string outFilename)
		{
			if (!Directory.Exists("Converse/"))
				Directory.CreateDirectory("Converse/");
			List<WordModel> words = new List<WordModel>();
			string[] lines = File.ReadAllLines(path);
			foreach (string line in lines)
			{
				string[] data = line.Split('\t');
				int valence = int.Parse(data[1]);
				words.Add(new WordModel(data[0], valence));
			}
			File.WriteAllText($"Converse/{outFilename}.json" ,JsonConvert.SerializeObject(words));

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
			File.WriteAllText($"Converse/results-{session}.json", JsonConvert.SerializeObject(results));
			return baseAmity;
		}

		private void ShizukaConverse(SocketUserMessage e)
		{
			Console.WriteLine($"Sentiment: {EvaluateSentiment(e.Content)} -> \"{e.Content}\"");

			string message = e.Content;
			SocketUser user = e.Author;
			SocketGuildChannel channel = e.Channel as SocketGuildChannel;
		}

		public override Task Respond(SocketUserMessage message)
		{

			return Task.CompletedTask;
		}
	}
}
