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

	public sealed class ExpLogical : Expression
	{

		public Expression Left;
		public Expression Right;
		public Token Operator;

		public ExpLogical(Expression left, Expression right, Token op)
		{
			Left = left;
			Right = right;
			Operator = op;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public dynamic Cast(Sandbox sb)
		{
			dynamic left = Left.Cast(sb);

			if(Operator.Type == TokenType.OR)
			{
				if(left) return left;
			}
			else
			{
				if(!left) return left;
			}

			return Right.Cast(sb);
		}

	}

}
