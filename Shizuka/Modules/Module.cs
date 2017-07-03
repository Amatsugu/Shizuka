using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Shizuka.Models;

namespace Shizuka.Modules
{
    public abstract class Module
    {
		public string Name { get; protected set; }
		public string HelpMessage { get; protected set; }
		public int Priority { get; protected set; }

		public Module(string name, string helpMessage = "No help message")
		{
			Name = name;
			HelpMessage = helpMessage;
			Priority = 0;
		}

		public abstract void Init(Server server);

		public abstract ModuleModel GetModel();

		public abstract Task Respond(SocketUserMessage message);

		public virtual string GetHelpMessage(string[] args) => $"{Name}: \n\t{HelpMessage}";
    }
}
