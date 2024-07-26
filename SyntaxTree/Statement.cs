﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;

namespace TwiaSharp.SyntaxTree
{

	public interface Statement
	{

		public Union Execute();

		public void AppendE(Statement s) { }
		
		public void AppendL(Statement s) { }

	}

}
