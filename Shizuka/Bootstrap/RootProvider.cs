using Nancy;
using System.IO;

namespace Shizuka.Bootstrap
{
#if DEBUG
	public class RootProvider : IRootPathProvider
	{
		public string GetRootPath()
		{
			return Directory.GetCurrentDirectory();
		}
	}
#endif
}