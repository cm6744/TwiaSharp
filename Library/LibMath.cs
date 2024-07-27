using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using Twita.Maths;

namespace TwiaSharp.Library
{

	public class LibMath : Lib
	{

		public static string Name = "math";

		public override void Load() 
		{ 
			LibName = Name;

			Functions["sin"] = new Function((v) => Math.Sin(v.d(0)));
			Functions["cos"] = new Function((v) => Math.Cos(v.d(0)));
			Functions["tan"] = new Function((v) => Math.Tan(v.d(0)));
			Functions["rad"] = new Function((v) => Mth.Rad(v.f(0)));
			Functions["deg"] = new Function((v) => Mth.Deg(v.f(0)));
			Functions["clamp"] = new Function((v) => Math.Clamp(v.d(0), v.d(1), v.d(2)));
			Functions["log10"] = new Function((v) => Math.Log10(v.d(0)));
			Functions["log2"] = new Function((v) => Math.Log2(v.d(0)));
			Functions["sqrt"] = new Function((v) => Math.Sqrt(v.d(0)));
			Functions["abs"] = new Function((v) => Math.Abs(v.d(0)));
			Functions["atan"] = new Function((v) => Math.Atan(v.d(0)));
			Functions["atan2"] = new Function((v) => Math.Atan2(v.d(0), v.d(1)));
			Functions["max"] = new Function((v) => Math.Max(v.d(0), v.d(1)));
			Functions["min"] = new Function((v) => Math.Min(v.d(0), v.d(1)));
			Functions["ceil"] = new Function((v) => Math.Ceiling(v.d(0)));
			Functions["floor"] = new Function((v) => Math.Floor(v.d(0)));
			Functions["round"] = new Function((v) => Math.Round(v.d(0)));

			Consts["math_pi"] = Math.PI;
			Consts["math_e"] = Math.E;
		}

	}

}
