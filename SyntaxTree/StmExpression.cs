using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio.OpenAL;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;

namespace TwiaSharp.SyntaxTree
{

	public sealed class StmExpression : Statement
	{

		public readonly Expression Value;

		public StmExpression(Expression value)
		{
			Value = value;
		}

		public Union Execute()
		{
			return Value.Cast();
		}

	}

}
