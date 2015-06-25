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

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		unsafe internal static extern sbyte* cmark_version_string();

		unsafe public static string StringVersion {
			get {
				return new string(cmark_version_string());
			}
		}

		unsafe public static Version Version {
			get {
				return new Version(StringVersion);
			}
		}
	}
}

