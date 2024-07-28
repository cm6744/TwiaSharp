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

		public readonly Dictionary<string, object> Fields = new();

		public UnionObject Property(string s, object o)
		{
			Fields[s] = o;
			return this;
		}

		public UnionObject Method(string s, Function fn)
		{
			Fields[s] = fn;
			return this;
		}

		public object Pack()
		{
			return Fields;
		}

	}

}
