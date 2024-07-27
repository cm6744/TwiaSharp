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
			
		}

	}

}
