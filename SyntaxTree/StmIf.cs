using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TwiaSharp.Runtime;

namespace TwiaSharp.SyntaxTree
{

	public sealed class StmIf : Statement
	{

		public readonly Expression Condition;
		public readonly Statement ThenBranch;
		public readonly Statement ElseBranch;

		public StmIf(Expression condition, Statement thenBranch, Statement elseBranch)
		{
			Condition = condition;
			ThenBranch = thenBranch;
			ElseBranch = elseBranch;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Union Execute()
		{
			if(Condition.Cast().Bol)
			{
				return ThenBranch.Execute();
			}
			else if(ElseBranch != null)
			{
				return ElseBranch.Execute();
			}
			return Union.Null;
		}

	}

}
