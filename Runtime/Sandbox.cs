using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Library;
using TwiaSharp.SyntaxTree;

namespace TwiaSharp.Runtime
{

	public static class Sandbox
	{

		public static LibOverall LibOverall;

		public static void LoadEnvironment()
		{
			LibOverall = new LibOverall();
			LibOverall.Link(new LibSystem());
			LibOverall.Link(new LibMath());
			LibOverall.Link(new LibIO());
		}

		public static SandboxStack Stack = new();
		public static Union[] Functions;
		
		public const int FrameDepth = 64;
		public const int FrameSize = 64;
		public static int Depth = -1;
		
		public static SandboxEndpoint Endpoint = new SandboxEndpoint.ConsoleEndpoint();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Push(Expression[] args)
		{
			for(int i = 0, j = args.Length; i < j; i++)
			{
				Stack[Depth + 1, i] = args[i].Cast();
			}
		}

		//Only Functions can create a scope.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Union _D_InnerExecute(List<Statement> statements)
		{
			Depth++;
			foreach(var stmt in statements)
			{
				Union u = stmt.Execute();
				if(u.Return)
				{
					u.Return = false;
					//A function invocation should not pass return out.
					Depth--;
					return u;
				}
			}
			Depth--;
			return Union.Null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Union __InnerExecute(List<Statement> statements)
		{
			foreach(var stmt in statements)
			{
				Union u = stmt.Execute();
				if(u.Return)
				{
					return u;
				}
			}
			return Union.Null;
		}

		public static void Terminate()
		{
			Depth = -1;
		}

	}

}
