﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;

namespace TwiaSharp.SyntaxTree
{

    public sealed class StmBlock : Statement
    {

        public readonly List<Statement> Statements;

		public StmBlock(List<Statement> statements)
		{
			Statements = statements;
		}

		public Union Execute()
		{
			return Sandbox.__InnerExecute(Statements);
		}

		public void AppendE(Statement s) 
		{ 
			Statements.Add(s);
		}

		public void AppendL(Statement s) 
		{ 
			Statements.Insert(0, s);
		}

	}

}