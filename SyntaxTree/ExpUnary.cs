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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Union Cast()
		{
			Union right = Right.Cast();

			if(right.FirstType == 0)
			{
				switch(Operator.Type)
				{
					case TokenType.MINUS: return Union.Of(-right.Num);
				}
			}
			if(right.FirstType == 8)
			{
				switch(Operator.Type)
				{
					case TokenType.BANG: return Union.Of(!right.Bol);
				}
			}
			if(right.FirstType == 16)
			{
				switch(Operator.Type)
				{
					case TokenType.MINUS: return Union.Of(-right.Obj);
					case TokenType.BANG: return Union.Of(!right.Obj);
				}
			}

			return Union.Null;
		}

	}

}
