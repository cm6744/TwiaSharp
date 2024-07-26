using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;

namespace TwiaSharp.SyntaxTree
{

	public sealed class StmExpression : Statement
	{

		public readonly Expression Expr;

		public StmExpression(Expression expr)
		{
			Expr = expr;
		}

		public Union Execute()
		{
			return Expr.Cast();
		}

	}

}
