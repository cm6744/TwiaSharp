using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwiaSharp.Syntax
{

	public class Token
	{

		public TokenType Type;
		public string Lexeme;
		public object Literal;
		public int Line;

		public Token(TokenType type, string lex, object litrl, int ln)
		{
			Type = type;
			Lexeme = lex;
			Literal = litrl;
			Line = ln;
		}

		public override string ToString()
		{
			return $"[{Type} {Lexeme} {Literal}]";
		}

	}

}
