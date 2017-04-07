using System;
using System.Collections.Generic;
using System.Text;
using Discord;

namespace Shizuka.Modules
{
    public abstract class Module
    {
		public string name { get; protected set; }
		public string helpMessage { get; protected set; }
		public int priority { get; protected set; }

		public Module(string name, string helpMessage = "No help message")
		{
			this.name = name;
			this.helpMessage = helpMessage;
			priority = 0;
		}

		public abstract void Init(Server server);

		public abstract void Respond(Message message);

		public virtual string GetHelpMessage(string[] args) => $"{name}: \n\t{helpMessage}";
    }
}
