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

	public sealed class StmReturn : Statement
	{

		public readonly Token Keyword;
		public readonly Expression Value;

		public StmReturn(Token keyword, Expression value)
		{
			Keyword = keyword;
			Value = value;
		}

		public Union Execute()
		{
			Union u = Value.Cast();
			u.Return = true;
			return u;
		}

	}

}
