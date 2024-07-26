using System;
using TwiaSharp.Runtime;

namespace TwiaSharp.Library
{

	public class Function : FunctionLike
	{

		public delegate object FunctionBody(Driver driver);

		public FunctionBody Body;

		public Function(FunctionBody body)
		{
			Body = body;
		}

		public virtual Union Invoke(Driver driver)
		{
			return Union.FromObject(Body.Invoke(driver));
		}

	}

}
