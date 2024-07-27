using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TwiaSharp.SyntaxTree;
using Twita.Common;

namespace TwiaSharp.Runtime
{

	public enum TypeDef
	{
		
		UNDEF = 0,
		NUM = 1,
		BOOL = 8,
		OBJECT = 16,
		EXPR = 17,
		VOID = 32,

	}

	public unsafe class Union
	{

		//UNION ITSELF

		public byte FirstType;
		public bool Return;
		public float _Umg;
		public object _Obj;

		public float Num => _Umg;
		public bool Bol => _Umg == 1;
		//Here's a recursion that goes into the deepest literal value.
		public dynamic Obj => ToObject(this);

		Union(float ptr, TypeDef def)
		{
			_Umg = ptr;
			FirstType = (byte) def;
			Return = false;
		}

		public static Union Of(float v)
		{
			return new Union(v, TypeDef.NUM);
		}

		public static Union Of(bool v)
		{
			return new Union(v ? 1 : 0, TypeDef.BOOL);
		}

		public static Union Of(object v)
		{
			return new Union(0, TypeDef.OBJECT) { _Obj = v };
		}

		public static Union Of(object v, TypeDef type)
		{
			return new Union(0, type) { _Obj = v };
		}

		public static readonly Union Null = new(0, TypeDef.VOID);

		public static Union FromObject(object o)
		{
			if(o == null) return Null;

			return o switch
			{
				int d3 => Of((float) d3),
				float d4 => Of((float) d4),
				double d5 => Of((float) d5),
				bool b => Of((bool) b),
				Supplier<object> os => Of(os, TypeDef.EXPR),
				_ => Of(o)
			};
		}

		public static object ToObject(Union u)
		{
			if(u == null) return null;

			return u.FirstType switch
			{
				(byte) TypeDef.NUM => u.Num,
				(byte) TypeDef.BOOL => u.Bol,
				(byte) TypeDef.OBJECT => u._Obj,
				(byte) TypeDef.EXPR => ((Supplier<object>) u._Obj).Invoke(),
				(byte) TypeDef.VOID => null,

				_ => u.Obj,
			};
		}

		public static Union Certained(Union u)
		{
			if(u.FirstType == 17) 
				return Union.FromObject(u.Obj);
			return u;
		}

		public static string EnsureS(object o)
		{
			if(o == null) return "Null";
			return o is string s ? s : o.ToString();
		}

	}

}
