using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Newtonsoft.Json;
using Shizuka.Modules.Keywords;

namespace Shizuka.Modules
{
	public class ImageResponseModule : Module
	{
		private List<ImageResponse> _images;

		public ImageResponseModule() : base("image")
		{
			helpMessage = "";
		}

		public override void Init(Server server)
		{ 
			_images = JsonConvert.DeserializeObject<List<ImageResponse>>(File.ReadAllText(@"C:\Users\Karuta\Documents\GitHub\Shizuka\images.json"));
			foreach (ImageResponse i in _images)
				server.RegisterKeyword(new Keyword(i.name, this, -1));
		}

		public override void Respond(Message m)
		{
			if(_images.Any(x => x.name == m.Text))
				m.Channel.SendMessage(_images.First(x => x.name == m.Text).GetImage());
		}

		public override string GetHelpMessage(string[] args)
		{
			return base.GetHelpMessage(args);
		}
	}
}
