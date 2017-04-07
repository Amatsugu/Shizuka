using System;
using System.Collections.Generic;
using System.Text;

namespace Shizuka.Modules.Keywords
{
    public struct Keyword
    {
		public string Key { get; private set; }
		public Module Target { get; private set; }
		public int Priority { get; private set; }

		public Keyword(string keyword, Module target, int priority = 0)
		{
			this.Key = keyword;
			this.Target = target;
			this.Priority = priority;
		}

		public static bool operator== (Keyword left, string right)
		{
			return (left.Key == right);
		}

		public static bool operator ==(Keyword left, Keyword right)
		{
			return (left.Key == right.Key);
		}

		public static bool operator !=(Keyword left, Keyword right)
		{
			return !(left == right);
		}

		public static bool operator!= (Keyword left, string right)
		{
			return !(left.Key == right);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (obj.GetType() != typeof(Keyword))
				return false;
			return this == ((Keyword)obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
