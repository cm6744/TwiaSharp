using System;
using TwiaSharp.Runtime;

namespace TwiaSharp.Library
{

	public unsafe class Function : FunctionLike
	{

		public delegate object FunctionBody(Driver driver);

		public FunctionBody Body;

		public Function(FunctionBody body)
		{
			Body = body;
		}

		public virtual object Invoke(Driver driver)
		{
			return Body.Invoke(driver);
		}

	}

}
