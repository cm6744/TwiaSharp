using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;

namespace TwiaSharp.SyntaxTree
{

	public sealed class ExpVarArray : Expression
	{

		public int Access;
		public Expression Key;
		
		public ExpVarArray(int ac, Expression key)
		{
			Key = key;
			Access = ac;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public dynamic Cast(Sandbox sb)
		{
			dynamic arr = sb[sb.Depth, Access];

			if(arr is object[])
				return arr[(int) Key.Cast(sb)];
			else
				return arr[Key.Cast(sb)];
		}

	}

}
