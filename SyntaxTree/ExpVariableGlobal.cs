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

	public sealed class ExpVariableGlobal : Expression
	{

		public int Access;
		
		public ExpVariableGlobal(int ac)
		{
			Access = ac;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Union Cast()
		{
			return Sandbox.Functions[Access];
		}

	}

}
