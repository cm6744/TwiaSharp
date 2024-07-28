using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Library;
using TwiaSharp.SyntaxTree;

namespace TwiaSharp.Runtime
{

	public class Sandbox
	{

		public static LibOverall LibOverall;
		public static int FrameDepth = 64;
		public static int FrameSize = 64;

		public static void LoadEnvironment()
		{
			LibOverall = new LibOverall();
			LibOverall.Link(new LibSystem());
			LibOverall.Link(new LibMath());
			LibOverall.Link(new LibIO());
			LibOverall.Link(new LibVisual());
		}

		//INST

		public object[,] Stack = new object[FrameDepth, FrameSize];

		public dynamic this[int d, int k]
		{
			get
			{
				return Stack[d, k];
			}
			set
			{
				Stack[d, k] = value;
			}
		}

		public FunctionLike[] Functions;
		public bool Return;

		public int Depth = -1;
		
		public SandboxEndpoint Endpoint = new SandboxEndpoint.ConsoleEndpoint();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Push(Expression[] args)
		{
			for(int i = 0, j = args.Length; i < j; i++)
			{
				Stack[Depth + 1, i] = args[i].Cast(this);
			}
		}

		//Only Functions can create a scope.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public object _D_InnerExecute(List<Statement> statements)
		{
			Depth++;
			foreach(var stmt in statements)
			{
				object u = stmt.Execute(this);
				if(Return)
				{
					Return = false;
					//A function invocation should not pass return out.
					Depth--;
					return u;
				}
			}
			Depth--;
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public object __InnerExecute(List<Statement> statements)
		{
			foreach(var stmt in statements)
			{
				object u = stmt.Execute(this);
				if(Return)
				{
					return u;
				}
			}
			return null;
		}

		public void Terminate()
		{
			Depth = -1;
		}

	}

}
