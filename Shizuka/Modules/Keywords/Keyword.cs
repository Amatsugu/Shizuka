using System;
using System.Collections.Generic;
using System.Text;

namespace Shizuka.Modules.Keywords
{
    public struct Keyword
    {
		public string keyword { get; private set; }
		public Module target { get; private set; }
		public int priority { get; private set; }

		public Keyword(string keyword, Module target, int priority = 0)
		{
			this.keyword = keyword;
			this.target = target;
			this.priority = priority;
		}

		public static bool operator== (Keyword left, string right)
		{
			return (left.keyword == right);
		}

		public static bool operator ==(Keyword left, Keyword right)
		{
			return (left.keyword == right.keyword);
		}

		public static bool operator !=(Keyword left, Keyword right)
		{
			return !(left == right);
		}

		public static bool operator!= (Keyword left, string right)
		{
			return !(left.keyword == right);
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
