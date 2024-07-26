using System;
using System.Collections.Generic;
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

	public unsafe sealed class Union
	{

		//UNION ITSELF

		public byte FirstType;
		public bool Return;
		
		public double Val0;
		public dynamic Obj0;

		public double Num => Val0;
		public bool Bol => Val0 == 1;
		//Here's a recursion that goes into the deepest literal value.
		public dynamic Obj => ToObject(this);

		static Union GetUnion(TypeDef type)
		{
			return new Union() { FirstType = (byte) type };
		}

		public static Union Of(double v)
		{
			Union u = GetUnion(TypeDef.NUM);
			u.Val0 = v;
			return u;
		}

		public static Union Of(bool v)
		{
			Union u = GetUnion(TypeDef.BOOL);
			u.Val0 = v ? 1 : 0;
			return u;
		}

		public static Union Of(double v, TypeDef type)
		{
			Union u = GetUnion(type);
			u.Val0 = v;
			return u;
		}

		public static Union Of(bool v, TypeDef type)
		{
			Union u = GetUnion(type);
			u.Val0 = v ? 1 : 0;
			return u;
		}

		public static Union Of(object v)
		{
			Union u = GetUnion(TypeDef.OBJECT);
			u.Obj0 = v;
			return u;
		}

		public static Union Of(object v, TypeDef type)
		{
			Union u = GetUnion(type);
			u.Obj0 = v;
			return u;
		}

		public static readonly Union Null = Of(null, TypeDef.VOID);

		public static Union FromObject(object o)
		{
			if(o == null) return Null;

			return o switch
			{
				int d3 => Of(d3, TypeDef.NUM),
				float d4 => Of(d4, TypeDef.NUM),
				double d5 => Of(d5, TypeDef.NUM),
				bool b => Of(b, TypeDef.BOOL),
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
				(byte) TypeDef.OBJECT => u.Obj0,
				(byte) TypeDef.EXPR => ((Supplier<object>) u.Obj0).Invoke(),
				(byte) TypeDef.VOID => null,

				_ => u.Obj,
			};
		}

		public static Union Certained(Union u)
		{
			if(u.FirstType == 17) return Union.FromObject(u.Obj);
			return u;
		}

	}

}
