using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using Twita.Codec;
using Twita.Maths;

namespace TwiaSharp.Library
{

	public class LibSystem : Lib
	{

		public override void Load() 
		{ 
			LibName = "system";

			Functions["sizeof"] = new Function((v) =>
			{
				object o = v[0];
				if(o is byte) return sizeof(byte);
				if(o is int) return sizeof(int);
				if(o is float) return sizeof(float);
				if(o is double) return sizeof(double);
				if(o is bool) return sizeof(bool);
				return -1;
			});
			Functions["len"] = new Function((v) =>
			{
				object o = v[0];
				if(o is Union[] _a1) return _a1.Length;
				if(o is UnionObject _a2) return _a2.Fields.Count;
				if(o is IEnumerable<Union> _a3) return _a3.Count();
				return 0;
			});
			Functions["compile_string"] = new Function((v) => CompiledChunk.From((string) v[0]));
			Functions["compile_file"] = new Function((v) => CompiledChunk.From((FileHandler) v[0]));
			Functions["execute"] = new Function((v) =>
			{
				CompiledChunk cc = v[0];
				if(v.Length == 1)
				{
					cc.Eval();
					return null;
				}
				object[] o = new object[v.Length - 1];
				for(int i = 1; i < v.Length; i++) o[i - 1] = v[i];
				cc.Eval(o);
				return null;
			});
			Functions["eval_to"] = new Function((v) =>
			{
				CompiledChunk cc = v[0];
				if(v.Length == 2)
				{
					cc.EvalTo(v[1]);
					return null;
				}
				object[] o = new object[v.Length - 2];
				for(int i = 2; i < v.Length; i++) o[i - 2] = v[i];
				cc.EvalTo(v[1], o);
				return null;
			});
		}

	}

}
