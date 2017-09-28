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
				try
				{
					return View["server", Shizuka.GetServerData((ulong)args.server)];
				}catch
				{
					return new Response { StatusCode = HttpStatusCode.NotFound };
				}
			});
		}
    }
}
