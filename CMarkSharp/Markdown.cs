using System;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public class Markdown
	{
		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_version();

		public static int IntegerVersion {
			get {
				return cmark_version();
			}
		}
	}
}

