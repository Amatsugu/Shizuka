using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Discord;
using Discord.Audio;
using Shizuka.Auth;
using Shizuka.Modules;
using Shizuka.Modules.Keywords;
using System.Threading.Tasks;

namespace Shizuka
{
	public static class Shizuka
    {
		

		private static DiscordClient _client;
		private static IAudioClient _audioClient;
		private static Dictionary<ulong, Server> _servers;

		public static async void Init()
		{
			_servers = new Dictionary<ulong, Server>();
			_client = new DiscordClient();
			await _client.Connect(Credentials.Token, TokenType.Bot);
			_client.MessageReceived += MessageReceived;
			_client.SetGame("something strange");
			/*_client.UsingAudio(x => x.Mode = AudioMode.Outgoing);
			try
			{
				var c = _client.GetServer(200113511232307200);
				//_audioClient = await _client.GetService<AudioService>().Join(c);

			}catch(Exception e)
			{
				Console.Write(e.Message);
			}*/
			InitModules();
		}

#region Initialization

		private static void InitModules()
		{
			foreach (Server s in _servers.Values)
				s.InitModules();
		}
#endregion

		

#region EventHandlers
		public static void MessageReceived(object sender, MessageEventArgs e)
		{
			if (!_servers.ContainsKey(e.Server.Id))
				_servers.Add(e.Server.Id, new Server(e.Server.Id).InitModules());
			if (e.User.IsBot)
				return;
			if (e.Message.IsAuthor)
				return;
			_servers[e.Server.Id].MessageReceived(sender, e);
		}
#endregion

		internal static void Close()
		{
			_audioClient?.Disconnect();
			var t = _client.Disconnect();
			Task.WaitAll(t);
			_client.Dispose();
		}
	}
}
