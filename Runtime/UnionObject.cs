using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Library;

namespace TwiaSharp.Runtime
{

	public class UnionObject
	{

		public readonly Dictionary<string, Union> Fields = new();

		public UnionObject Property(string s, object o)
		{
			Fields[s] = Union.FromObject(o);
			return this;
		}

		public UnionObject Method(string s, Function fn)
		{
			Fields[s] = Union.FromObject(fn);
			return this;
		}

		public Union Pack()
		{
			return Union.Of(Fields);
		}

	}

}
