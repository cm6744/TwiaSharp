using System;
using System.Collections.Generic;
using TwiaSharp.Runtime;
using TwiaSharp.SyntaxTree;

namespace TwiaSharp.Library
{

	public class Driver
	{

		public Expression[] Args;
		public Sandbox _Sandbox;

		public Driver(Expression[] args)
		{
			Args = args;
		}

		public Driver()
		{
			Args = Array.Empty<Expression>();
		}

		public Driver With(Sandbox sb)
		{
			_Sandbox = sb;
			return this;
		}

		public int Length => Args.Length;

		public int i(int index)
		{
			dynamic u = Args[index].Cast(_Sandbox);
			return (int) u;
		}

		public float f(int index)
		{
			dynamic u = Args[index].Cast(_Sandbox);
			return (float) u;
		}

		public double d(int index)
		{
			dynamic u = Args[index].Cast(_Sandbox);
			return u;
		}

		public string s(int index)
		{
			dynamic u = Args[index].Cast(_Sandbox);
			return Union.EnsureS(u);
		}

		public T t<T>(int index)
		{
			dynamic u = Args[index].Cast(_Sandbox);
			return (T) u;
		}

		public dynamic any(int index)
		{
			dynamic u = Args[index].Cast(_Sandbox);
			return u;
		}

	}

}
