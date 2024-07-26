using System.Collections.Generic;
using TwiaSharp.Runtime;
using TwiaSharp.SyntaxTree;

namespace TwiaSharp.Library
{

	public class FunctionDynamic : FunctionLike
	{

		public List<Statement> Stmts;

		public FunctionDynamic(List<Statement> stmts)
		{
			Stmts = stmts;
		}

		public FunctionDynamic()
		{
			Stmts = new();
		}

		public virtual Union Invoke(Driver driver)
		{
			Sandbox.Push(driver.Args);
			return Sandbox._D_InnerExecute(Stmts);
		}

	}

}
