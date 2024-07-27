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

		public int Length => Args.Length;

		public int i(int index)
		{
			Union u = Args[index].Cast();
			return (int) u.Num;
		}

		public float f(int index)
		{
			Union u = Args[index].Cast();
			return (float) u.Num;
		}

		public double d(int index)
		{
			Union u = Args[index].Cast();
			return u.Num;
		}

		public string s(int index)
		{
			Union u = Args[index].Cast();
			return Union.EnsureS(Union.ToObject(u));
		}

		public T t<T>(int index)
		{
			Union u = Args[index].Cast();
			return (T) Union.ToObject(u);
		}

		public dynamic any(int index)
		{
			Union u = Args[index].Cast();
			return (dynamic) Union.ToObject(u);
		}

	}

}
