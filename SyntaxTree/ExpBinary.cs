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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Union Cast()
		{
			Union right = Right.Cast();
			Union left = Left.Cast();
			byte t1 = right.FirstType;
			byte t2 = left.FirstType;

			if(t1 == 1 && t2 == 1)
			{
				switch(Operator.Type)
				{
					case TokenType.MINUS: return Union.Of(left.Num - right.Num);
					case TokenType.PLUS: return Union.Of(left.Num + right.Num);
					case TokenType.STAR: return Union.Of(left.Num * right.Num);
					case TokenType.SLASH: return Union.Of(left.Num / right.Num);
					case TokenType.GRT: return Union.Of(left.Num > right.Num);
					case TokenType.GRT_EQ: return Union.Of(left.Num >= right.Num);
					case TokenType.LES: return Union.Of(left.Num < right.Num);
					case TokenType.LES_EQ: return Union.Of(left.Num <= right.Num);
					case TokenType.EQ_EQ: return Union.Of(left.Num == right.Num);
					case TokenType.BANG_EQ: return Union.Of(left.Num != right.Num);
				}
			}

			if(t1 == 8 && t2 == 8)
			{
				switch(Operator.Type)
				{
					case TokenType.EQ_EQ: return Union.Of(left.Bol == right.Bol);
					case TokenType.BANG_EQ: return Union.Of(left.Bol != right.Bol);
				}
			}

			if(t1 == 32 || t2 == 32)//If here's a null
			{
				switch(Operator.Type)
				{
					case TokenType.EQ_EQ: return Union.Of(left.FirstType == right.FirstType);
					case TokenType.BANG_EQ: return Union.Of(left.FirstType != right.FirstType);
				}
				return Union.Null;
			}

			return Operator.Type switch
			{
				TokenType.MINUS => Union.Of(left.Obj - right.Obj),
				TokenType.PLUS => Union.Of(left.Obj + right.Obj),
				TokenType.STAR => Union.Of(left.Obj * right.Obj),
				TokenType.SLASH => Union.Of(left.Obj / right.Obj),
				TokenType.GRT => Union.Of(left.Obj > right.Obj),
				TokenType.GRT_EQ => Union.Of(left.Obj >= right.Obj),
				TokenType.LES => Union.Of(left.Obj < right.Obj),
				TokenType.LES_EQ => Union.Of(left.Obj <= right.Obj),
				TokenType.EQ_EQ => Union.Of(left.Obj == right.Obj),
				TokenType.BANG_EQ => Union.Of(left.Obj != right.Obj),
				_ => Union.Null,
			};
		}

	}

}
