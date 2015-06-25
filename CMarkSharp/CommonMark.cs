using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public class CommonMark
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
				// TODO: make options settable
				return new string(cmark_markdown_to_html(ptr, bytes.Length, 0));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_parse_document(IntPtr buffer, int len, int options);

		#region ParseDocument

		// TODO: options

		public static Node ParseDocument(IntPtr buffer, int length, int options)
		{
			return new Node(cmark_parse_document(buffer, length, options));
		}

		unsafe public static Node ParseDocument(byte[] buffer, int index, int count, int options)
		{
			Ensure.InRange(buffer, nameof(buffer), index, nameof(index), count, nameof(count));

			fixed (byte* ptr = buffer) {
				return ParseDocument((IntPtr)ptr + index, count, options);
			}
		}

		public static Node ParseDocument(byte[] buffer, int options)
		{
			Ensure.ArgumentNotNull(buffer, nameof(buffer));

			return ParseDocument(buffer, 0, buffer.Length, options);
		}

		public static Node ParseDocument(ArraySegment<byte> byteSegment, int options)
		{
			Ensure.ArgumentNotDefault(byteSegment, nameof(byteSegment));
			return ParseDocument(byteSegment.Array, byteSegment.Offset, byteSegment.Count, options);
		}

		public static Node ParseDocument(string text, int options, Encoding encoding = null)
		{
			if (encoding == null) {
				encoding = Encoding.Default;
			}

			return ParseDocument(encoding.GetBytes(text), options);
		}

		#endregion

		#region File

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_parse_file(IntPtr file, int options);

		public static Node ParseFile(IntPtr fileHandle, int options)
		{
			return new Node(cmark_parse_file(fileHandle, options));
		}

		public static Node ParseFile(System.IO.FileStream fileStream, int options)
		{
			return ParseFile(fileStream.SafeFileHandle.DangerousGetHandle(), options);
		}

		#endregion
	}
}

