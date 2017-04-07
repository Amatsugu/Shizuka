using System;
using System.Linq;
using System.Collections.Generic;
using Discord.WebSocket;

namespace Shizuka.Modules.Converse
{
	internal struct ResponseMap
	{
		public List<string> Inputs { get; private set; }
		public Action<SocketUserMessage> Target { get; private set; }
		public int Priority { get; private set; }

		public ResponseMap(IEnumerable<string> inputs, Action<SocketUserMessage> target, int priority = 0)
		{
			Inputs = new List<string>();
			foreach (string i in inputs)
				Inputs.Add(i.ToLower());
			Target = target;
			Priority = priority;
		}

		public bool Match(string input) => Inputs.Any(x => x.Contains(input.ToLower()));
	}
}