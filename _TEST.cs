using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;
using TwiaSharp.SyntaxTree;
using Twita.Codec;
using Twita.Codec.General;

namespace TwiaSharp
{
	class _TEST
	{

		static void Main(string[] _)
		{
			FileSystem.AsApplicationSource();
			FileSystem.AsTestSource();

			Sandbox.LoadEnvironment();

			CompiledChunk.From(FileSystem.GetLocal("FIB.twi"));
			var exp = CompiledChunk.From(FileSystem.GetLocal("EXAMPLE.twi"));

			var l1 = DateTime.Now;
			exp.Eval();
			var l2 = DateTime.Now;

			Console.WriteLine((l2 - l1).TotalMilliseconds);

			l1 = DateTime.Now;
			exp.Eval();
			l2 = DateTime.Now;

			Console.WriteLine((l2 - l1).TotalMilliseconds);

			l1 = DateTime.Now;
			exp.Eval();
			l2 = DateTime.Now;

			Console.WriteLine((l2 - l1).TotalMilliseconds);

			l1 = DateTime.Now;
			exp.Eval();
			l2 = DateTime.Now;

			Console.WriteLine((l2 - l1).TotalMilliseconds);

			l1 = DateTime.Now;
			exp.Eval();
			l2 = DateTime.Now;

			Console.WriteLine((l2 - l1).TotalMilliseconds);
		}

	}
}
