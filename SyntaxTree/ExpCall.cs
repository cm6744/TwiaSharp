using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Library;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;

namespace TwiaSharp.SyntaxTree
{

	public sealed class ExpCall : Expression
	{

        public readonly Expression Callee;
        public Driver Arguments;//Set aside

        public ExpCall(Expression callee, Expression[] args)
        {
            Callee = callee;
            Arguments = new Driver(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public dynamic Cast(Sandbox sb)
        {
            FunctionLike fn = Callee.Cast(sb);
            if(fn == null)
            {
                Errors.RuntimeError(-1, $"Unknown function.");
			}
            return fn.Invoke(Arguments.With(sb));
        }

    }

}
