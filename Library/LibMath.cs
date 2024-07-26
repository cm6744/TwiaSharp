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

		public override void Load() 
		{ 
			LibName = "math";

			Functions["sin"] = new Function((v) => Math.Sin((double) v[0]));
			Functions["cos"] = new Function((v) => Math.Cos((double) v[0]));
			Functions["tan"] = new Function((v) => Math.Tan((double) v[0]));
			Functions["rad"] = new Function((v) => Mth.Rad((float) v[0]));
			Functions["deg"] = new Function((v) => Mth.Deg((float) v[0]));
			Functions["clamp"] = new Function((v) => Math.Clamp((double) v[0], (double) v[1], (double) v[2]));
			Functions["log10"] = new Function((v) => Math.Log10((double) v[0]));
			Functions["log2"] = new Function((v) => Math.Log2((double) v[0]));
			Functions["sqrt"] = new Function((v) => Math.Sqrt((double) v[0]));
			Functions["abs"] = new Function((v) => Math.Abs((double) v[0]));
			Functions["atan"] = new Function((v) => Math.Atan((double) v[0]));
			Functions["atan2"] = new Function((v) => Math.Atan2((double) v[0], (double) v[1]));
			Functions["max"] = new Function((v) => Math.Max((double) v[0], (double) v[1]));
			Functions["min"] = new Function((v) => Math.Min((double) v[0], (double) v[1]));
			Functions["ceil"] = new Function((v) => Math.Ceiling((double) v[0]));
			Functions["floor"] = new Function((v) => Math.Floor((double) v[0]));
			Functions["round"] = new Function((v) => Math.Round((double) v[0]));

			Consts["math_pi"] = Math.PI;
			Consts["math_e"] = Math.E;
		}

	}

}
