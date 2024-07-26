using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Library;
using TwiaSharp.Syntax;
using TwiaSharp.SyntaxTree;
using Twita.Codec;
using Twita.Codec.General;

namespace TwiaSharp.Runtime
{

	public sealed class CompiledChunk
	{

		public static CompiledChunk From(FileHandler file)
		{
			string code = StringIO.Read(file);
			var lst = TokenScan.ToStream(code);
			return TreeBuild.ToTree(lst);
		}

		public static CompiledChunk From(string code)
		{
			var lst = TokenScan.ToStream(code);
			return TreeBuild.ToTree(lst);
		}

		public Union[] FnTemp = new Union[256];
		public FunctionLike InitFunc;
		public Lib Library;

		public Union Eval()
		{
			Sandbox.Functions = FnTemp;
			Union u = InitFunc.Invoke(Driver.DR0);
			Sandbox.Terminate();
			return u;
		}

		public Union Eval(params object[] objs)
		{
			Expression[] exps = new Expression[objs.Length];

			for(int i = 0; i < objs.Length; i++)
			{
				exps[i] = new ExpLiteral(objs[i]);
			}

			Sandbox.Functions = FnTemp;
			Union u = InitFunc.Invoke(new Driver(exps));
			Sandbox.Terminate();
			return u;
		}

		public Union EvalTo(string fn)
		{
			Sandbox.Functions = FnTemp;
			Union u = Library.Search(fn).Invoke(Driver.DR0);
			Sandbox.Terminate();
			return u;
		}

		public Union EvalTo(string fn, params object[] objs)
		{
			Expression[] exps = new Expression[objs.Length];

			for(int i = 0; i < objs.Length; i++)
			{
				exps[i] = new ExpLiteral(objs[i]);
			}

			Sandbox.Functions = FnTemp;
			Union u = Library.Search(fn).Invoke(new Driver(exps));
			Sandbox.Terminate();
			return u;
		}

	}

}
