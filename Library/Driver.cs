using System.Collections.Generic;
using TwiaSharp.Runtime;
using TwiaSharp.SyntaxTree;

namespace TwiaSharp.Library
{
	public class Driver
	{

		public static Driver DR0 = new Driver(new Expression[0]);

		public Expression[] Args;

		public Driver(Expression[] args)
		{
			Args = args;
		}

		public dynamic this[int index] => Union.ToObject(Args[index].Cast());
		public int Length => Args.Length;

	}

}
