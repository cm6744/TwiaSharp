using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;

namespace TwiaSharp.SyntaxTree
{

	public sealed class StmOutStream : Statement
	{

		public readonly Expression Expr;

		public StmOutStream(Expression expr)
		{
			Expr = expr;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Union Execute()
		{
			Sandbox.Endpoint.Outstream(Union.ToObject(Expr.Cast()));
			return Union.Null;
		}

	}

}
