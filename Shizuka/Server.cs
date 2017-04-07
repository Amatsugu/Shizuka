using Shizuka.Modules;
using Shizuka.Modules.Keywords;
using System;
using System.Linq;
using System.Collections.Generic;
using Discord;
using System.Text;

namespace Shizuka
{
    public class Server
    {
		public event EventHandler<MessageEventArgs> messageReceived;
		public event EventHandler<MessageEventArgs> shizukaMentioned;

		public ulong id { set; private get; }
		public HashSet<Module> Modules { set; private get; }
		private List<Keyword> _keywords;


		public Server(ulong id)
		{
			this.id = id;
			Modules = new HashSet<Module>();
			_keywords = new List<Keyword>();
			LoadModules();
		}

		public void LoadModules()
		{
			Modules.Add(new ImageResponseModule());
		}

		public Server InitModules()
		{
			foreach (Module module in Modules)
				module.Init(this);
			_keywords = (from k in _keywords orderby k.priority select k).ToList();
			return this;
		}


		public void RegisterKeyword(Keyword keyword)
		{
			if (!_keywords.Any(x => x == keyword))
				_keywords.Add(keyword);
		}

		internal void MessageReceived(object sender, MessageEventArgs e)
		{
			if (e.Message.IsMentioningMe(true))
				shizukaMentioned.Invoke(sender, e);
			else
			{
				string m = e.Message.Text;
				if(m.Contains(' '))
					messageReceived.Invoke(sender, e);
				else
				{
					_keywords.First(x => x == m).target?.Respond(e.Message);
				}
			}
		}
	}
}
