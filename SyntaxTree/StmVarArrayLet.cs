using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using TwiaSharp.Syntax;

namespace TwiaSharp.SyntaxTree
{

	public sealed class StmVarArrayLet : Statement
	{

        public int Access;
        public Expression Key;
        public Expression Val;

        public StmVarArrayLet(int ac, Expression key, Expression val)
        {
            Key = key;
            Access = ac;
            Val = val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Union Execute()
        {
            dynamic arr = Sandbox.Stack[Sandbox.Depth, Access].Obj;

            if(arr is Union[])
                return arr[(int) Key.Cast().Num] = Val.Cast();
            else
                return arr[Union.ToObject(Key.Cast())] = Val.Cast();
        }

    }

}
