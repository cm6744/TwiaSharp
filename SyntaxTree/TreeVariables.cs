using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;

namespace TwiaSharp.SyntaxTree
{

	public class TreeVariables
	{

		public readonly Dictionary<string, Dictionary<string, int>> FunctionalVarlist = new();
		public readonly Dictionary<string, int> VarIncreaser = new();
		public static readonly Dictionary<string, int> GVarlist = new();
		public static int GVarIncreaser;
		public static int ClosureId;

		public string func => TreeBuild.CURRENT_FUNCTION_SCOPE;

		public int Make(string code, bool gen = true)
		{
			if(string.IsNullOrEmpty(code)) return -1;
			if(!FunctionalVarlist.ContainsKey(func)) FunctionalVarlist[func] = new();
			if(!VarIncreaser.ContainsKey(func)) VarIncreaser[func] = 0;
			if(FunctionalVarlist[func].TryGetValue(code, out var i))
			{
				return i;
			}
			if(!gen)
			{
				return -1;
			}
			int l = FunctionalVarlist[func][code] = VarIncreaser[func];
			VarIncreaser[func]++;
			return l;
		}

		public static int MakeG(string code, bool gen = true)
		{
			if(string.IsNullOrEmpty(code)) return -1;

			if(GVarlist.TryGetValue(code, out var i))
			{
				return i;
			}
			if(!gen)
			{
				return -1;
			}
			int l = GVarlist[code] = GVarIncreaser;
			GVarIncreaser++;
			return l;
		}

		public static string NextClosure()
		{
			return $"%_CLOSURE_SCOPE_{ClosureId++}_%";
		}

	}

}
