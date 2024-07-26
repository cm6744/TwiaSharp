using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twita.Codec;

namespace TwiaSharp.Syntax
{

	public class TokenScan
	{

		static List<Token> list;
		static int start = 0, current = 0, line = 1;
		static string src;

		public static List<Token> ToStream(string code)
		{
			list = new();
			start = 0;
			current = 0;
			line = 1;
			src = code;

			while(!End)
			{
				start = current;
				Scan();
			}

			list.Add(new Token(TokenType.EOF, "", null, line));

			return list;
		}

		static void Scan()
		{
			char ch = src[current++];
			switch(ch)
			{
				case '(': Push(TokenType.L_PAREN); break;
				case ')': Push(TokenType.R_PAREN); break;
				case '{': Push(TokenType.L_BRACE); break;
				case '}': Push(TokenType.R_BRACE); break;
				case '[': Push(TokenType.L_SEMBR); break;
				case ']': Push(TokenType.R_SEMBR); break;
				case ':': Push(TokenType.COLON); break;
				case ',': Push(TokenType.COMMA); break;
				case '.': Push(TokenType.DOT); break;
				case '-': Push(TokenType.MINUS); break;
				case '+': Push(TokenType.PLUS); break;
				case '*': Push(TokenType.STAR); break;
				case '/': 
					if(Match('/')) 
					{
						while(Peek() != '\n' && !End) current++; break;
					}
					Push(TokenType.SLASH); break;
				case '!': Push(Match('=') ? TokenType.BANG_EQ : TokenType.BANG); break;
				case '>': Push(Match('=') ? TokenType.GRT_EQ : TokenType.GRT); break;
				case '<': Push(Match('=') ? TokenType.LES_EQ : TokenType.LES); break;
				case '=': Push(Match('=') ? TokenType.EQ_EQ : TokenType.EQ); break;
				case '#': while(Peek() != '\n' && !End) current++; break;
				case ';': Push(TokenType.SEMCOL); break;
				case ' ':
				case '\t':
				case '\r':
					break;
				case '\n': line++; break;
				case '"': BpString(); break;
				default: 
					if(IsDigit(ch)) BpNum();
					else if(IsAlpbtc(ch)) BpIdent();
					else Errors.SyntaxError(line, $"Unexpected Character: {ch}"); 
					break;
			}
		}

		static bool End => current >= src.Length;

		static bool IsIdentable(char ch) { return IsDigit(ch) || IsAlpbtc(ch); }

		static bool IsDigit(char ch) { return ch >= '0' && ch <= '9'; }
		
		static bool IsAlpbtc(char ch) { return ch >= 'a' && ch <= 'z' || ch >= 'A' && ch <= 'Z' || ch == '_'; }
		
		static void Push(TokenType type) { Push(type, null); }
		
		static void Push(TokenType type, object litrl) { string txt = src.Substring(start, current - start); list.Add(new Token(type, txt, litrl, line)); }
		
		static bool Match(char exp)
		{
			if(End) return false;
			if(src[current] != exp) return false;
			current++;
			return true;
		}
		
		static char Peek()
		{
			if(End) return '\0';
			return src[current];
		}
		
		static char PeekNext()
		{
			if(current + 1 >= src.Length) return '\0';
			return src[current + 1];
		}
		
		static void BpString()
		{
			while(Peek() != '"' && !End)
			{
				if(Peek() == '\n') line++;
				current++;
			}
			if(End) Errors.SyntaxError(line, "String is not terminated!");
			current++;//jump over '"'.
			string sub = src.Substring(start + 1, current - start - 2);
			Push(TokenType.STR, sub);
		}
		
		static void BpNum()
		{
			while(IsDigit(Peek())) current++;
			if(Peek() == '.' && IsDigit(PeekNext()))
			{
				current++;
				while(IsDigit(Peek())) current++;
			}
			string sub = src.Substring(start, current - start);
			Push(TokenType.NUM, LayeredSeek(sub));
			return;

			static object LayeredSeek(string code)
			{
				if(double.TryParse(code, out double r))
				{
					return r;
				}
				return null;
			}
		}
		
		static void BpIdent()
		{
			while(IsIdentable(Peek())) current++;
			string sub = src.Substring(start, current - start);
			TokenType type = Keywords.GetValueOrDefault(sub, TokenType.EOF);
			if(type == TokenType.EOF) type = TokenType.IDENT;
			Push(type);
		}

		static Dictionary<string, TokenType> Keywords = new();

		static TokenScan()
		{
			Keywords["or"] = TokenType.OR;
			Keywords["and"] = TokenType.AND;
			//Keywords["class"] = TokenType.CLASS;
			Keywords["else"] = TokenType.ELSE;
			Keywords["false"] = TokenType.FALSE;
			Keywords["for"] = TokenType.FOR;
			Keywords["function"] = TokenType.FUNC;
			Keywords["if"] = TokenType.IF;
			Keywords["void"] = TokenType.VOID;
			Keywords["return"] = TokenType.RET;
			//Keywords["basic"] = TokenType.BASIC;
			//Keywords["here"] = TokenType.HERE;
			Keywords["true"] = TokenType.TRUE;
			Keywords["let"] = TokenType.LET;
			Keywords["while"] = TokenType.WHILE;
			Keywords["begin"] = TokenType.BEGIN;
			Keywords["end"] = TokenType.END;
			Keywords["do"] = TokenType.DO;
			//Keywords["var"] = TokenType.VAR;
			Keywords["array"] = TokenType.ARRAY;
			Keywords["new"] = TokenType.NEW;
			Keywords["import"] = TokenType.IMPT;
			Keywords["export"] = TokenType.EXPT;
			Keywords["const"] = TokenType.CONST;
		}

	}

}
