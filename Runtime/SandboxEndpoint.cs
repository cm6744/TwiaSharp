using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwiaSharp.Runtime
{

	public interface SandboxEndpoint
	{

		public void Outstream(object o);

		public class ConsoleEndpoint : SandboxEndpoint
		{

			public void Outstream(object o)
			{
				Console.WriteLine(Union.EnsureS(o));
			}

		}

	}

}
