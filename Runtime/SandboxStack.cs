using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwiaSharp.Runtime
{

	public sealed class SandboxStack
	{

		public Union[,] Values = new Union[Sandbox.FrameDepth, Sandbox.FrameSize];
		
		public Union this[int d, int k]
		{
			get 
			{ 
				return Values[d, k] ?? Union.Null;
			}
			set 
			{
				Values[d, k] = value;
			}
		}

	}

}
