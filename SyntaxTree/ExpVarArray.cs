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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Union Cast()
		{
			dynamic arr = Sandbox.Stack[Sandbox.Depth, Access].Obj;

			if(arr is Union[])
				return arr[(int) Key.Cast().Num];
			else
				return arr[Union.ToObject(Key.Cast())];
		}

	}

}
