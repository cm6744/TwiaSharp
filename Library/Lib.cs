using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;

namespace TwiaSharp.Library
{

	public class Lib
	{

		public string LibName;
		public Dictionary<string, FunctionLike> Functions = new();
		public Dictionary<string, object> Consts = new();

		public virtual FunctionLike Search(string name)
		{
			return Functions.GetValueOrDefault(name, null);
		}

		public virtual object TryConst(string name)
		{
			return Consts.GetValueOrDefault(name, null);
		}

		public virtual void Load() { }

	}

}
