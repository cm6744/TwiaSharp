using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;
using Twita.Common;

namespace TwiaSharp.SyntaxTree
{

    public sealed class ExpLiteral : Expression
    {

        public Union Value;

        public ExpLiteral(object value)
        {
            Value = Union.FromObject(value);
        }

        public ExpLiteral(Supplier<object> value)
        {
            Value = Union.FromObject(value);
        }

        public Union Cast()
        {
            //Do not let the supplier go into stack!!!
            return Union.Certained(Value);
        }

    }

}
