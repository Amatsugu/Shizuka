using System;
using System.Collections.Generic;
using System.Text;

namespace Shizuka.Models
{
    public class ServerModel
    {
		public ulong ID { get; }
		public string Name { get; }
		public ModuleModel[] Modules { get; }

		public ServerModel(ulong id, string name, ModuleModel[] modules)
		{
			ID = id;
			Name = name;
			Modules = modules;
		}
	}
}
