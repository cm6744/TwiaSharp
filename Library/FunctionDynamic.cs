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

		public virtual object Invoke(Driver driver)
		{
			driver._Sandbox.Push(driver.Args);
			return driver._Sandbox._D_InnerExecute(Stmts);
		}

	}

}
