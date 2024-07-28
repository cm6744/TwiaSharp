using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.SyntaxTree;

namespace TwiaSharp.Runtime
{

	public class Union
	{

		public static string EnsureS(object o)
		{
			if(o == null) return "Null";
			return o is string s ? s : o.ToString();
		}

	}

}
