using Shizuka.Modules;
using Shizuka.Modules.Keywords;
using System;
using System.Linq;
using System.Collections.Generic;
using Discord.WebSocket;
using System.Text;
using System.Threading.Tasks;
using Shizuka.Modules.Image;
using Shizuka.Modules.Converse;

namespace Shizuka
{
    public class Server
    {
		public event Action<SocketUserMessage> MessageReceived;
		public event Action<SocketUserMessage> ShizukaMentioned;

		public ulong ID { set; private get; }
		public HashSet<Module> Modules { set; private get; }
		private List<Keyword> _keywords;


		public Server(ulong id)
		{
			this.ID = id;
			Modules = new HashSet<Module>();
			_keywords = new List<Keyword>();
			LoadModules();
		}

		public void LoadModules()
		{
			Modules.Add(new ImageResponseModule());
			Modules.Add(new ConversationModule());
		}

		public Server InitModules()
		{
			foreach (Module module in Modules)
				module.Init(this);
			_keywords = (from k in _keywords orderby k.Priority descending select k).ToList();
			return this;
		}


		public void RegisterKeyword(Keyword keyword)
		{
			if (!_keywords.Any(x => x == keyword))
				_keywords.Add(keyword);
		}

		internal Task Received(SocketUserMessage e)
		{
			if (e.MentionedUsers.Any(x => e.Discord.CurrentUser.Id == x.Id))
				ShizukaMentioned.Invoke(e);
			else
			{
				var m = e.Content;
				if(m[0] == '!')
					_keywords.First(x => x == m).Target?.Respond(e);
				else
					MessageReceived.Invoke(e);
			}
			return Task.CompletedTask;
		}
	}
}
