using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;
using Twita.Common;

namespace TwiaSharp.SyntaxTree
{

    public sealed class ExpLiteral : Expression
    {

        public object Value;
        private bool Sup;

        public ExpLiteral(object value)
        {
            Value = value;
        }

        public ExpLiteral(Supplier<object> value)
        {
            Value = value;
            Sup = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public dynamic Cast(Sandbox sb)
        {
            if(Sup) return ((Supplier<object>) Value).Invoke();
            //Do not let the supplier go into stack!!!
            return Value;
        }

    }

}
