using Nancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shizuka
{
	public class ServerWebModule : NancyModule
    {
		public ServerWebModule() : base("/s")
		{
			Get("/{server}", args =>
			{
				return View["server"];
			});
		}
    }
}
