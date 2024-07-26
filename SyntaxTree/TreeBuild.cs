﻿using System.Collections.Generic;
using System.Xml.Linq;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TwiaSharp.Library;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;

namespace TwiaSharp.SyntaxTree
{

	public class TreeBuild
	{

		public static string CURRENT_FUNCTION_SCOPE;

		static List<Token> tokens;
		static int current;
		static TreeVariables vars;
		static Lib localLib;
		static Dictionary<string, FunctionDynamic> funcBodies;
		static List<string> usings;

		public static CompiledChunk ToTree(List<Token> tokenslist)
		{
			tokens = tokenslist;
			current = 0;
			CURRENT_FUNCTION_SCOPE = "%_GLOBAL_SCOPE_%";
			vars = new();
			localLib = new();
			funcBodies = new();
			usings = new();

			//PRECOMIPILE

			bool export = false;

			while(!End)
			{
				if(Match(TokenType.IMPT))
				{
					Token name = Consume(TokenType.IDENT);
					usings.Add(name.Lexeme);
				}
				else if(Match(TokenType.EXPT))
				{
					Token name = Consume(TokenType.IDENT);
					localLib.LibName = name.Lexeme;
					export = true;
				}
				else if(Match(TokenType.CONST))
				{
					Token name = Consume(TokenType.IDENT);
					Consume(TokenType.EQ);
					Expression val = exp();
					localLib.Consts[name.Lexeme] = Union.ToObject(val.Cast());
				}
				else if(Match(TokenType.FUNC))
				{
					//Add a function prototype
					Token name = Consume(TokenType.IDENT);
					FunctionDynamic dyn = new FunctionDynamic();
					funcBodies[name.Lexeme] = dyn;
					localLib.Functions[name.Lexeme] = dyn;
				}
				else Advance();
			}

			current = 0;

			//END

			//COMPILE

			CompiledChunk code = new();

			current = 0;

			while(!End)
			{
				if(Match(TokenType.FUNC))
				{
					function_def();
				}
				else Advance();
			}

			code.Library = localLib;
			code.InitFunc = localLib.Search("init");

			if(export)
			{
				Sandbox.LibOverall.Link(localLib);
			}

			//Put all functions into global vars.
			foreach(var kv in localLib.Functions)
			{
				code.FnTemp[TreeVariables.MakeG(kv.Key)] = Union.Of(kv.Value);
			}
			foreach(var libn in usings)
			{
				Lib lib = Sandbox.LibOverall.Vars[libn];
				foreach(var kv in lib.Functions)
				{
					code.FnTemp[TreeVariables.MakeG(kv.Key)] = Union.Of(kv.Value);
				}
			}

			return code;
		}

		//STATEMENTS

		static void function_def()
		{
			Token name = Consume(TokenType.IDENT);
			Consume(TokenType.L_PAREN);
			List<Token> parameters = new();

			CURRENT_FUNCTION_SCOPE = name.Lexeme;

			if(!Check(TokenType.R_PAREN))
			{
				do
				{
					Token tk = Consume(TokenType.IDENT);
					parameters.Add(tk);
					vars.Make(tk.Lexeme);
				}
				while(Match(TokenType.COMMA));
			}

			Consume(TokenType.R_PAREN);

			Consume(TokenType.DO);

			List<Statement> body = get_block();

			CURRENT_FUNCTION_SCOPE = "%_GLOBAL_SCOPE_%";

			funcBodies[name.Lexeme].Stmts = body;
		}

		static Statement stm(bool needSemcol = true)
		{
			if(Match(TokenType.RET)) return stm_ret();
			if(Match(TokenType.IF)) return stm_if();
			if(Match(TokenType.FOR)) return stm_for();
			if(Match(TokenType.WHILE)) return stm_while();
			if(Match(TokenType.DO)) return new StmBlock(get_block());
			if(Match(TokenType.LET)) return stm_letvar(needSemcol);

			return stm_fromexp(needSemcol);
		}

		static Statement stm_letvar(bool needSemcol = true)
		{
			Token name = Consume(TokenType.IDENT);
			Expression vexp = null;
			Expression idxr = null;
			Token key = null;

			if(Match(TokenType.L_SEMBR))
			{
				idxr = exp();
				Consume(TokenType.R_SEMBR);
			}
			if(Match(TokenType.DOT))
			{
				key = Consume(TokenType.IDENT);
			}
			if(Match(TokenType.EQ)) vexp = exp();

			if(needSemcol) Consume(TokenType.SEMCOL);

			int i = vars.Make(name.Lexeme);

			if(idxr != null)
				return new StmVarArrayLet(i, idxr, vexp);
			if(key != null)
				return new StmVarObjectLet(i, key, vexp);
			return new StmVarLet(i, vexp);
		}

		static Statement stm_ret()
		{
			Token keyword = Previous;
			Expression value = null;

			if(!Check(TokenType.SEMCOL)) value = exp();

			Consume(TokenType.SEMCOL);

			return new StmReturn(keyword, value);
		}

		static Statement stm_for()
		{
			Consume(TokenType.L_PAREN);

			//p1
			Statement init;
			if(Match(TokenType.SEMCOL)) init = null;
			else if(Match(TokenType.LET)) init = stm_letvar();
			else init = stm_fromexp();

			//p2
			Expression cond;
			if(!Check(TokenType.SEMCOL)) cond = exp();
			else cond = new ExpLiteral(true);

			Consume(TokenType.SEMCOL);

			//p3
			Statement inc = null;
			if(!Check(TokenType.R_PAREN)) inc = stm(false);

			Consume(TokenType.R_PAREN);

			Statement then = stm();

			if(inc != null) then.AppendE(inc);

			then = new StmWhile(cond, then);

			List<Statement> lst = new();

			if(init != null) lst.Add(init);

			lst.Add(then);

			return new StmBlock(lst);
		}

		static Statement stm_while()
		{
			Consume(TokenType.L_PAREN);
			Expression cond = exp();
			Consume(TokenType.R_PAREN);

			Statement then = stm();

			return new StmWhile(cond, then);
		}

		static Statement stm_if()
		{
			Consume(TokenType.L_PAREN);
			Expression cond = exp();
			Consume(TokenType.R_PAREN);

			Statement then = stm();
			Statement @else = null;
			if(Match(TokenType.ELSE)) @else = stm();

			return new StmIf(cond, then, @else);
		}

		static List<Statement> get_block()
		{
			List<Statement> lst = new();

			while(!Check(TokenType.END) && !End)
			{
				lst.Add(stm());
			}

			Consume(TokenType.END);
			return lst;
		}

		static Statement stm_fromexp(bool needSemcol = true)
		{
			Expression expr = exp();
			if(needSemcol)
				Consume(TokenType.SEMCOL);
			return new StmExpression(expr);
		}

		//EXPRESSION

		static Expression exp()
		{
			return exp_or();
		}

		static Expression exp_or()
		{
			Expression expr = exp_and();

			while(Match(TokenType.OR))
			{
				Token opt = Previous;
				Expression right = exp_eq();
				expr = new ExpLogical(expr, right, opt);
			}

			return expr;
		}

		static Expression exp_and()
		{
			Expression expr = exp_eq();

			while(Match(TokenType.AND))
			{
				Token opt = Previous;
				Expression right = exp_eq();
				expr = new ExpLogical(expr, right, opt);
			}

			return expr;
		}

		static Expression exp_eq()
		{
			Expression exp = exp_neq();

			while(Match(TokenType.EQ_EQ, TokenType.BANG_EQ))
			{
				Token opt = Previous;
				Expression r = exp_neq();
				exp = new ExpBinary(exp, r, opt);
			}

			return exp;
		}

		static Expression exp_neq()
		{
			Expression exp = exp_opt1();

			while(Match(TokenType.GRT, TokenType.GRT_EQ, TokenType.LES, TokenType.LES_EQ))
			{
				Token opt = Previous;
				Expression r = exp_opt1();
				exp = new ExpBinary(exp, r, opt);
			}

			return exp;
		}

		static Expression exp_opt1()
		{
			Expression exp = exp_opt2();

			while(Match(TokenType.MINUS, TokenType.PLUS))
			{
				Token opt = Previous;
				Expression r = exp_opt2();
				exp = new ExpBinary(exp, r, opt);
			}

			return exp;
		}

		static Expression exp_opt2()
		{
			Expression exp = exp_unary();

			while(Match(TokenType.SLASH, TokenType.STAR))
			{
				Token opt = Previous;
				Expression r = exp_opt2();
				exp = new ExpBinary(exp, r, opt);
			}

			return exp;
		}

		static Expression exp_unary()
		{
			if(Match(TokenType.MINUS, TokenType.BANG))
			{
				Token opt = Previous;
				Expression r = exp_unary();
				return new ExpUnary(r, opt);
			}

			return exp_call();
		}

		static Expression exp_call()
		{
			Expression expr = exp_primary();

			while(true)
			{
				if(Match(TokenType.L_PAREN)) expr = _fin_call(expr);
				else break;
			}

			return expr;
		}

		static Expression exp_primary()
		{
			if(Match(TokenType.FALSE)) return new ExpLiteral(false);
			if(Match(TokenType.TRUE)) return new ExpLiteral(true);
			if(Match(TokenType.VOID)) return new ExpLiteral(null);

			if(Match(TokenType.NUM, TokenType.STR)) return new ExpLiteral(Previous.Literal);
			if(Match(TokenType.IDENT))
			{
				Token ident = Previous;
				int i = vars.Make(ident.Lexeme, false);

				if(i == -1)
				{
					var _c1 = Sandbox.LibOverall.SearchConsts(usings, ident.Lexeme);
					var _c2 = localLib.TryConst(ident.Lexeme);

					//local priors to libs.
					if(_c2 != null) return new ExpLiteral(_c2);
					if(_c1 != null) return new ExpLiteral(_c1);

					return new ExpVariableGlobal(TreeVariables.MakeG(ident.Lexeme));
				}

				if(Match(TokenType.L_SEMBR))
				{
					Expression idxr = exp();
					Consume(TokenType.R_SEMBR);
					return new ExpVarArray(i, idxr);
				}

				//We do not support 'chain' dot. (like a.b.c)
				if(Match(TokenType.DOT))
				{
					Token key = Consume(TokenType.IDENT);
					return new ExpVarObject(i, key);
				}

				return new ExpVariable(i);
			}

			//let x = new array[i];
			if(Match(TokenType.NEW) && Match(TokenType.ARRAY))
			{
				Consume(TokenType.L_SEMBR);
				Expression size = exp();
				Consume(TokenType.R_SEMBR);
				//Support dynamic size input
				return new ExpLiteral(() => new Union[(int) size.Cast().Num]);
			}

			//let x = [1, 2, 3, ...];
			if(Match(TokenType.L_SEMBR))
			{
				List<Union> joins = new();

				if(!Check(TokenType.R_SEMBR))
				{
					do
					{
						joins.Add(exp().Cast());
					}
					while(Match(TokenType.COMMA));
				}

				Consume(TokenType.R_SEMBR);

				return new ExpLiteral(joins.ToArray());
			}

			//let x = { a = 1, b = 2 };
			if(Match(TokenType.L_BRACE))
			{
				UnionObject joins = new();

				if(!Check(TokenType.R_BRACE))
				{
					do
					{
						Token tk = Consume(TokenType.IDENT);
						Consume(TokenType.EQ);
						//Only support fixed value to initalize
						joins.Fields[tk.Lexeme] = exp().Cast();
					}
					while(Match(TokenType.COMMA));
				}

				Consume(TokenType.R_BRACE);

				return new ExpLiteral(joins);
			}

			//Bracketed expression
			if(Match(TokenType.L_PAREN))
			{
				Expression expr = exp();
				Consume(TokenType.R_PAREN);
				return expr;
			}

			return null;
		}

		static Expression _fin_call(Expression callee)
		{
			List<Expression> args = new();

			if(!Check(TokenType.R_PAREN))
			{
				do
				{
					args.Add(exp());
				}
				while(Match(TokenType.COMMA));
			}

			Consume(TokenType.R_PAREN);

			return new ExpCall(callee, args.ToArray());
		}

		//UTILS

		static bool End => Peek.Type == TokenType.EOF;

		static Token Peek => tokens[current];

		static Token Previous => tokens[current - 1];

		static Token Advance() { if(!End) current++; return Previous; }

		static bool Match(params TokenType[] types)
		{
			foreach(var type in types)
			{
				if(Check(type))
				{
					Advance();
					return true;
				}
			}
			return false;
		}

		static bool Check(TokenType type) { if(End) return false; return Peek.Type == type; }

		static Token Consume(TokenType type)
		{
			if(Check(type)) return Advance();
			Errors.SyntaxError(tokens[current].Line, $"'{Peek.Lexeme}'");
			current = tokens.Count - 1;//Wind it up.
			throw new System.Exception();
		}

	}

}
