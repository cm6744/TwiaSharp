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

	public sealed class ExpUnary : Expression
	{

		public Expression Right;
		public Token Operator;

		public ExpUnary(Expression right, Token op)
		{
			Right = right;
			Operator = op;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public dynamic Cast(Sandbox sb)
		{
			dynamic right = Right.Cast(sb);

			switch(Operator.Type)
			{
				case TokenType.MINUS: return (-right);
				case TokenType.BANG: return (!right);
			}

			return null;
		}

	}

}
