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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Shizuka.Bootstrap;

namespace Shizuka
{
	public static class Shizuka
    {
		public static string DataDir { get; private set; }
		public static ulong ID { get; private set; }

		private static DiscordSocketClient _client;
		private static Dictionary<ulong, Server> _servers;


		public static async Task Init()
		{
			DataDir = $"{Directory.GetCurrentDirectory()}/Data";
			Console.WriteLine(DataDir);
			_servers = new Dictionary<ulong, Server>();
			_client = new DiscordSocketClient();
			_client.Log += Log;
			_client.MessageReceived += MessageReceived;
			await _client.LoginAsync(TokenType.Bot, Credentials.Token);
			await _client.StartAsync();
			_client.Ready += async ()=>
			{
				await _client.SetGameAsync("with 2B");
				ID = _client.CurrentUser.Id;
				var host = new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory()).UseKestrel().UseStartup<NancyStartup>().Build();
				host.Run();
				Console.WriteLine("Running..");
			};
			InitModules();
		}


		public async static Task SendMessage(ulong channelID,string message, bool simulateTyping = true)
		{
			var ch = (_client.GetChannel(channelID) as SocketTextChannel);
			if (simulateTyping)
			{
				await ch.TriggerTypingAsync();
				await Task.Delay(50 * message.Length);
			}
			await ch.SendMessageAsync(message);
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
				_servers.Add(serverID, new Server(_client.GetGuild(serverID)).InitModules());
			if (e.Author.IsBot)
				return;
			await _servers[serverID].Received(e as SocketUserMessage);
		}
#endregion

		public static ServerData GetServerData(ulong id)
		{
			try
			{
				ServerData data = new ServerData(id, _servers[id].Name);
				return data;
			}catch
			{
				throw new Exception("No Such Server");
			}
		}

		internal static Task Log(LogMessage message)
		{
			Console.WriteLine(message.Message);
			return Task.CompletedTask;
		}

		internal static void Close()
		{
			_client.StopAsync().GetAwaiter().GetResult();
		}
	}
}
