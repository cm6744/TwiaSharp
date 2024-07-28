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

		public readonly List<(Expression, Statement)> Branches;
		public readonly Statement ElseBranch;

		public StmIf(List<(Expression, Statement)> branches, Statement elseBranch)
		{
			Branches = branches;
			ElseBranch = elseBranch;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public dynamic Execute(Sandbox sb)
		{
			foreach(var e_s in Branches)
			{
				if(e_s.Item1.Cast(sb)) return e_s.Item2.Execute(sb);
			}
			if(ElseBranch != null)
			{
				return ElseBranch.Execute(sb);
			}
			return null;
		}

	}

}
