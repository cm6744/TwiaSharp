using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TwiaSharp.Runtime;
using Twita.Common.Toolkit;

namespace TwiaSharp.Library
{

	public class LibOverall : Lib
	{

		public Dictionary<string, Lib> Vars = new();

		public void Hook(string lib, string name, Function fn)
		{
			Vars[lib].Functions[name] = fn;
		}

		public void Hook(string lib, string name, object cst)
		{
			Vars[lib].Consts[name] = cst;
		}

		public void Link(Lib lib)
		{
			lib.Load();

			if(lib.LibName == null) return;
			if(Vars.ContainsKey(lib.LibName)) return;

			Vars[lib.LibName] = lib;
		}

		public FunctionLike SearchAvailables(List<string> lst, string name)
		{
			foreach(string s in lst)
			{
				Lib lib = Vars[s];
				if(lib == null) Errors.RuntimeError(-1, $"Unknown library detected: {s}");
				FunctionLike func = lib.Search(name);
				if(func != null) return func;
			}
			return Search(name);
		}

		public object SearchConsts(List<string> lst, string name)
		{
			foreach(string s in lst)
			{
				Lib lib = Vars[s];
				if(lib == null) Errors.RuntimeError(-1, $"Unknown library detected: {s}");
				object o = lib.TryConst(name);
				if(o != null) return o;
			}
			return null;
		}

	}

}
