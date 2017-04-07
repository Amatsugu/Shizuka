using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shizuka.Modules
{
    public static class Extentions
    {
		public static string TextWithoutMention(this Message message)
		{
			string[] d = message.Text.Split(' ');
			return string.Join(" ", d, 1, d.Length - 1);
		}
	}
}
