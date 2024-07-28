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

	public sealed class StmVarObjectLet : Statement
	{

        public int Access;
        public Token Key;
        public Expression Val;

        public StmVarObjectLet(int ac, Token key, Expression val)
        {
            Key = key;
            Access = ac;
            Val = val;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public dynamic Execute(Sandbox sb)
        {
            UnionObject arr = sb[sb.Depth, Access];
            return arr.Fields[Key.Lexeme] = Val.Cast(sb);
        }

    }

}
