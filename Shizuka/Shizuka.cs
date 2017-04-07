using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Discord;
using Discord.Net.WebSockets;
using Shizuka.Auth;
using Shizuka.Modules;
using Shizuka.Modules.Keywords;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Shizuka
{
	public static class Shizuka
    {
		

		private static DiscordSocketClient _client;
		private static Dictionary<ulong, Server> _servers;

		public static async Task Init()
		{
			_servers = new Dictionary<ulong, Server>();
			_client = new DiscordSocketClient();
			_client.Log += Log;
			_client.MessageReceived += MessageReceived;
			await _client.LoginAsync(TokenType.Bot, Credentials.Token);
			await _client.StartAsync();
			_client.Ready += async ()=>
			{
				await _client.SetGameAsync("something strange");
			};
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
		public static async Task MessageReceived(SocketMessage e)
		{
			Console.WriteLine(((e as SocketUserMessage).Author as SocketGuildUser).Guild.Name);
			ulong serverID = ((e as SocketUserMessage).Author as SocketGuildUser).Guild.Id;
			if (!_servers.ContainsKey(serverID))
				_servers.Add(serverID, new Server(serverID).InitModules());
			if (e.Author.IsBot)
				return;
			await _servers[serverID].Received(e as SocketUserMessage);
		}
#endregion

		internal static Task Log(LogMessage message)
		{

			return Task.CompletedTask;
		}

		internal static async Task Close()
		{
			await _client.StopAsync();
			_client.Dispose();
		}
	}
}
