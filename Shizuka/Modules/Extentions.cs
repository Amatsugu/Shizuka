using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shizuka.Modules
{
    public static class Extentions
    {
		public static string TextWithoutMention(this SocketUserMessage message)
		{
			string[] d = message.Content.Split(' ');
			return string.Join(" ", d, 1, d.Length - 1);
		}
	}
}
