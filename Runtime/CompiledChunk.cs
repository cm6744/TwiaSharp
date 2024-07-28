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

		public FunctionLike[] FnTemp = new FunctionLike[256];
		public FunctionLike InitFunc;
		public Lib Library;

		public dynamic Eval(Sandbox sb)
		{
			sb.Functions = FnTemp;
			dynamic u = InitFunc.Invoke(new Driver().With(sb));
			sb.Terminate();
			return u;
		}

		public dynamic Eval(Sandbox sb, params object[] objs)
		{
			Expression[] exps = new Expression[objs.Length];

			for(int i = 0; i < objs.Length; i++)
			{
				exps[i] = new ExpLiteral(objs[i]);
			}

			sb.Functions = FnTemp;
			dynamic u = InitFunc.Invoke(new Driver(exps).With(sb));
			sb.Terminate();
			return u;
		}

		public dynamic EvalTo(Sandbox sb, string fn)
		{
			sb.Functions = FnTemp;
			dynamic u = Library.Search(fn).Invoke(new Driver().With(sb));
			sb.Terminate();
			return u;
		}

		public dynamic EvalTo(Sandbox sb, string fn, params object[] objs)
		{
			Expression[] exps = new Expression[objs.Length];

			for(int i = 0; i < objs.Length; i++)
			{
				exps[i] = new ExpLiteral(objs[i]);
			}

			sb.Functions = FnTemp;
			dynamic u = Library.Search(fn).Invoke(new Driver(exps).With(sb));
			sb.Terminate();
			return u;
		}

	}

}
