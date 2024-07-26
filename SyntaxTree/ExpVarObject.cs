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

	public sealed class ExpVarObject : Expression
	{

		public int Access;
		public Token Key;
		public Token Token;

		public ExpVarObject(int ac, Token key)
		{
			Key = key;
			Access = ac;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Union Cast()
		{
			UnionObject o = Sandbox.Stack[Sandbox.Depth, Access].Obj;
			return o.Fields[Key.Lexeme];
		}

	}

}
