using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Newtonsoft.Json;
using Shizuka.Modules.Keywords;
using Discord.WebSocket;
using System.Threading.Tasks;
using Shizuka.Models;

namespace Shizuka.Modules.Image
{
	public class ImageResponseModule : Module
	{
		private List<ImageResponse> _images;

		public ImageResponseModule() : base("image")
		{
			HelpMessage = "";
		}

		public override void Init(Server server)
		{ 
			_images = JsonConvert.DeserializeObject<List<ImageResponse>>(File.ReadAllText(@"C:\Users\Karuta\Documents\GitHub\Shizuka\images.json"));
			foreach (ImageResponse i in _images)
				server.RegisterKeyword(new Keyword(i.name, this, -1));
		}

		public override async Task Respond(SocketUserMessage m)
		{
			if(_images.Any(x => x.name == m.Content.Remove(0,1)))
				await m.Channel.SendMessageAsync(_images.First(x => x.name == m.Content.Remove(0,1)).GetImage());
		}

		public override string GetHelpMessage(string[] args)
		{
			return base.GetHelpMessage(args);
		}

		public override ModuleModel GetModel()
		{
			throw new NotImplementedException();
		}
	}
}
