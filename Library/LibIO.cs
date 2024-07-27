using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using Twita.Codec;
using Twita.Codec.General;
using Twita.Maths;

namespace TwiaSharp.Library
{

	public class LibIO : Lib
	{

		public static string Name = "io";

		public override void Load()
		{
			LibName = Name;

			Functions["fopen"] = new Function((v) => FileSystem.GetLocal(v.s(0)));
			Functions["fopen_abs"] = new Function((v) => FileSystem.GetAbsolute(v.s(0)));
			Functions["write_endpoint"] = new Function((v) =>
			{
				Sandbox.Endpoint.Outstream(v.any(0));
				return null;
			});
			Functions["read_strings_arrayed"] = new Function((v) => ToUArr(StringIO.ReadArray(v.any(0))));
			Functions["read_strings"] = new Function((v) => StringIO.Read(v.any(0)));
		}

		static Union[] ToUArr(object[] oarr)
		{
			Union[] uarr = new Union[oarr.Length];
			for(int i = 0; i < oarr.Length; i++) uarr[i] = Union.FromObject(oarr[i]);
			return uarr;
		}

	}

}
