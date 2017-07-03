using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Nancy.Owin;

namespace Shizuka.Bootstrap
{
    class NancyStartup
    {
		public void Configure(IApplicationBuilder app)
		{
			app.UseOwin(x => x.UseNancy());
		}
	}
}
