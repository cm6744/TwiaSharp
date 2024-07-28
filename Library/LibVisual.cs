using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwiaSharp.Runtime;
using Twita.Common;
using Twita.Draw;
using Twita.Maths.Structs;

namespace TwiaSharp.Library
{

	public class LibVisual : Lib
	{

		public static string Name = "visual";

		public override void Load()
		{
			LibName = Name;

			DrawBatch batch = Platform.Batch;

			Functions["push"] = new Function((v) =>
			{
				batch.Matrices.Push();
				return null;
			});
			Functions["pop"] = new Function((v) =>
			{
				batch.Matrices.Pop();
				return null;
			});
			Functions["rotate"] = new Function((v) =>
			{
				batch.Matrices.RotateDeg(v.f(0), v.f(1), v.f(2));
				return null;
			});
			Functions["rotate_rad"] = new Function((v) =>
			{
				batch.Matrices.RotateRad(v.f(0), v.f(1), v.f(2));
				return null;
			});
			Functions["translate"] = new Function((v) =>
			{
				batch.Matrices.Translate(v.f(0), v.f(1));
				return null;
			});
			Functions["scale"] = new Function((v) =>
			{
				batch.Matrices.Scale(v.f(0), v.f(1));
				return null;
			});
			Functions["color"] = new Function((v) =>
			{
				batch.Color4(new vec4(v.f(0), v.f(1), v.f(2), v.f(3)));
				return null;
			});
			Functions["normalize_color"] = new Function((v) =>
			{
				batch.NormalizeColor();
				return null;
			});
			Functions["draw"] = new Function((v) =>
			{
				if(v.Length == 3)
					batch.Draw(v.any(0), v.f(1), v.f(2));
				else if(v.Length == 5)
					batch.Draw(v.any(0), v.f(1), v.f(2), v.f(3), v.f(4));
				else if(v.Length == 9)
					batch.Draw(v.any(0), v.f(1), v.f(2), v.f(3), v.f(4), v.f(5), v.f(6), v.f(7), v.f(8));
				return null;
			});
			Functions["fill"] = new Function((v) =>
			{
				if(v.Length == 4)
					batch.FillTex(v.f(0), v.f(1), v.f(2), v.f(3));
				return null;
			});
			Functions["draw_text"] = new Function((v) =>
			{
				string s = v.s(0);
				if(v.Length == 3)
					batch.Draw(s, v.f(1), v.f(2));
				if(v.Length == 4)
					batch.Draw(s, v.f(1), v.f(2), (Align) v.f(3));
				if(v.Length == 5)
					batch.Draw(s, v.f(1), v.f(2), (Align) v.f(3), v.f(4));
				return null;
			});

			Consts["ALIGN_LEFT"] = Align.LEFT;
			Consts["ALIGN_CENTER"] = Align.CENTER;
			Consts["ALIGN_RIGHT"] = Align.RIGHT;
		}

	}

}
