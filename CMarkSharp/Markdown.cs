using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public class Markdown
	{
		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_version();

		public static int VersionInteger {
			get {
				return cmark_version();
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		unsafe internal static extern sbyte* cmark_version_string();

		unsafe public static string VersionString {
			get {
				return new string(cmark_version_string());
			}
		}

		unsafe public static Version Version {
			get {
				return new Version(VersionString);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		unsafe internal static extern sbyte* cmark_markdown_to_html(byte* ptr, int len, int options);

		unsafe public static string ToHtml(string text, Encoding encoding = null)
		{
			if (encoding == null) {
				encoding = Encoding.Default;
			}

			var bytes = encoding.GetBytes(text);
			fixed (byte* ptr = bytes) {
				// TODO: make options viable
				return new string(cmark_markdown_to_html(ptr, bytes.Length, 0));
			}
		}
	}
}

