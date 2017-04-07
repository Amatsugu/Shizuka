using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shizuka.Modules.Image
{
	internal class ImageResponse
	{
		public string name { get; private set; }
		public List<string> urls { get; private set; }
		public bool nsfw { get; private set; } = false;
		public string helpMessage { get; private set; }
		public static Random rand = new Random();

		[JsonConstructor]
		ImageResponse(string name, string[] urls, bool nsfw = false, string helpMessage = "")
		{
			this.name = name;
			this.urls = new List<string>();
			if(urls != null)
				this.urls.AddRange(urls);
			this.nsfw = nsfw;
			this.helpMessage = (helpMessage == null ? "" : helpMessage);
		}

		private ImageResponse(string name)
		{
			this.name = name;
			urls = new List<string>();
		}

		internal ImageResponse(string name, string url) : this(name)
		{
			urls.Add(url);
		}

		internal ImageResponse(string name, string[] urls) : this(name)
		{
			this.urls.AddRange(urls);
		}

		public string GetImage()
		{
			return urls[rand.Next(urls.Count - 1)];
		}
	}
}