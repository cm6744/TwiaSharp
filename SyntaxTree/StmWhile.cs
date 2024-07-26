using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;

namespace TwiaSharp.SyntaxTree
{

	public sealed class StmWhile : Statement
	{

		public readonly Expression Condition;
		public readonly Statement Body;

		public StmWhile(Expression condition, Statement body)
		{
			Condition = condition;
			Body = body;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public Union Execute()
		{
			while(Condition.Cast().Bol)
			{
				Union u = Body.Execute();
				if(u.Return)
				{
					return u;
				}
			}
			return Union.Null;
		}

	}

}
