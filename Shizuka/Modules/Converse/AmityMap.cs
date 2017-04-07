using System;
using System.Collections.Generic;
using System.Text;

namespace Shizuka.Modules.Converse
{
    public class AmityMap
    {
		public ulong UserID { get; private set; }
		public float Amity { get; set; }

		public AmityMap(ulong id, float amity = 0)
		{
			UserID = id;
			Amity = amity;
		}
    }
}
