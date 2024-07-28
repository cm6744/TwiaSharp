using System;
using System.Runtime.CompilerServices;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;

namespace TwiaSharp.SyntaxTree
{

	public sealed class ExpBinary : Expression
	{

		public Expression Left;
		public Expression Right;
		public Token Operator;

		public ExpBinary(Expression left, Expression right, Token op)
		{
			Left = left;
			Right = right;
			Operator = op;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public dynamic Cast(Sandbox sb)
		{
			dynamic right = Right.Cast(sb);
			dynamic left = Left.Cast(sb);

			//Greatly thanks to dynamic type.
			return Operator.Type switch
			{
				TokenType.MINUS => (left - right),
				TokenType.PLUS => (left + right),
				TokenType.STAR => (left * right),
				TokenType.SLASH => (left / right),
				TokenType.PERCENT => (left % right),
				TokenType.UPROW => (left ^ right),
				TokenType.GREAT => (left > right),
				TokenType.GREAT_EQ => (left >= right),
				TokenType.LESS => (left < right),
				TokenType.LESS_EQ => (left <= right),
				TokenType.EQ_EQ => (left == right),
				TokenType.BANG_EQ => (left != right),
				_ => null,
			};
		}

	}

}
