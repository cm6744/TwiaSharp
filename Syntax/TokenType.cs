using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwiaSharp.Syntax
{

	public enum TokenType
	{

		L_PAREN, 
		R_PAREN, 
		L_BRACE,
		R_BRACE,
		L_SEMBR,
		R_SEMBR,
		COLON,
		BEGIN, 
		DO,
		END,
		COMMA, 
		DOT, 
		MINUS, 
		PLUS, 
		SEMCOL, 
		SLASH, 
		STAR,

		BANG, 
		BANG_EQ, 
		EQ, 
		EQ_EQ, 
		GREAT, 
		GREAT_EQ, 
		LESS, 
		LESS_EQ,

		IDENT, 
		STR, 
		NUM,

		AND, 
		//CLASS, 
		ELSE, 
		FALSE, 
		FUNCTION, 
		WITH,
		FOR, 
		IF, 
		VOID, 
		OR, 
		RETURN, 
		//BASIC, 
		//HERE, 
		TRUE, 
		LET,
		WHILE,
		//VAR, 
		ARRAY,
		NEW,
		IMPORT,
		EXPORT,
		CONST,

		EOF

	}

}
