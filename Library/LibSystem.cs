using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using Twita.Codec;
using Twita.Common;
using Twita.Maths;

namespace TwiaSharp.Library
{

	public class LibSystem : Lib
	{

		public static string Name = "os";

		public override void Load() 
		{ 
			LibName = "os";

			Functions["sizeof"] = new Function((v) =>
			{
				object o = v.any(0);
				if(o is byte) return sizeof(byte);
				if(o is int) return sizeof(int);
				if(o is float) return sizeof(float);
				if(o is double) return sizeof(double);
				if(o is bool) return sizeof(bool);
				return -1;
			});
			Functions["len"] = new Function((v) =>
			{
				object o = v.any(0);
				if(o is Union[] _a1) return _a1.Length;
				if(o is UnionObject _a2) return _a2.Fields.Count;
				if(o is IEnumerable<Union> _a3) return _a3.Count();
				return 0;
			});

			Functions["frame_per_sec"] = new Function((v) => Platform.Fps);
			Functions["drawcall_per_tick"] = new Function((v) => Platform.Dpt);
			Functions["tick_per_sec"] = new Function((v) => Platform.Fps);
			Functions["ticks"] = new Function((v) => Platform.Ticks);
			Functions["timestamp"] = new Function((v) => DateTime.Now);
			Functions["timestamp_to_milli"] = new Function((v) =>
			{
				TimeSpan sp = v.any(0);
				return sp.TotalMilliseconds;
			});
		}

	}

}
