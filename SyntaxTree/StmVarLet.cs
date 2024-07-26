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

	public sealed class StmVarLet : Statement
	{

        public int Access;
        public Expression Val;

        public StmVarLet(int ac, Expression val)
        {
            Access = ac;
            Val = val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Union Execute()
        {
            return Sandbox.Stack[Sandbox.Depth, Access] = Val == null ? Union.Null : Val.Cast();
        }

    }

}
