using System;
using Twita.Common.Toolkit;

namespace TwiaSharp
{

	public class Errors
	{
		
		public static void SyntaxError(int ln, string w, string mes)
		{
			Log.Fatal($"Syntax Error: at line {ln} '{w}': {mes}");
		}

		public static void SyntaxError(int ln, string mes)
		{
			Log.Fatal($"Syntax Error: at line {ln}: {mes}");
		}

		public static void RuntimeError(int ln, string mes)
		{
			Log.Fatal($"Runtime Error: at line {ln}: {mes}");
		}

	}

}
