using System;
using System.Runtime.InteropServices;
using TwiaSharp.Runtime;

namespace TwiaSharp.Library
{

	public unsafe class FunctionUnsafe : FunctionLike
	{

		public delegate* managed<object, Driver> Body;

		public FunctionUnsafe(delegate* managed<object, Driver> body)
		{
			Body = body;
		}

		public virtual object Invoke(Driver driver)
		{
			return Body(driver);
		}

	}

}
