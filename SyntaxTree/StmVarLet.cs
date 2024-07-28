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

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public dynamic Execute(Sandbox sb)
        {
            return sb[sb.Depth, Access] = Val == null ? null : Val.Cast(sb);
        }

    }

}
