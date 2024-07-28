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

	public sealed class StmVarArrayLet : Statement
	{

        public int Access;
        public Expression Key;
        public Expression Val;

        public StmVarArrayLet(int ac, Expression key, Expression val)
        {
            Key = key;
            Access = ac;
            Val = val;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public dynamic Execute(Sandbox sb)
        {
            dynamic arr = sb[sb.Depth, Access];

            if(arr is object[])
                return arr[(int) Key.Cast(sb)] = Val.Cast(sb);
            else
                return arr[Key.Cast(sb)] = Val.Cast(sb);
        }

    }

}
