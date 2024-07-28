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
	class EXAMPLE
	{

		static void Main(string[] _)
		{
			FileSystem.AsApplicationSource();
			FileSystem.AsTestSource();

			Sandbox.LoadEnvironment();
			Sandbox sb = new();

			//Compile the dynamic lib.
			CompiledChunk.From(FileSystem.GetLocal("LIB_1.twi"));
			//Run the script.
			var exp = CompiledChunk.From(FileSystem.GetLocal("EXAMPLE.twi"));
			UnionObject obj = new UnionObject().Property("item1", "Hello world!");
			exp.Eval(sb, obj, DateTime.Now);
			//We can pass something into the script or invoke other functions.
			//CompiledChunk provides a serie of methods to fulfill you.

			//This script print the time used to for loop 1,000,000 times. Not slow.
			exp = CompiledChunk.From(FileSystem.GetLocal("TimeShow.twi"));
			exp.Eval(sb);
		}

	}
}
