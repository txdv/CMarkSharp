using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public class Parser : IDisposable
	{
		// TODO: look up options

		IntPtr pointer;

		protected Parser(IntPtr pointer)
		{
			this.pointer = pointer;
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_parser_new(int options);

		private Parser(int options)
			: this(cmark_parser_new(options))
		{
		}

		public Parser()
			: this(0)
		{
		}

		~Parser()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern void cmark_parser_free(IntPtr parser);

		protected void Dispose(bool disposing)
		{
			if (pointer != IntPtr.Zero) {
				cmark_parser_free(pointer);
				pointer = IntPtr.Zero;
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern void cmark_parser_feed(IntPtr parser, IntPtr buffer, int len);

		/// <summary>
		/// Feeds a pointer to a buffer with the set length to the parser.
		/// </summary>
		/// <param name="buffer">pointer to buffer</param>
		/// <param name="length">length of the buffer</param>
		public void Feed(IntPtr buffer, int length)
		{
			cmark_parser_feed(pointer, buffer, length);
		}

		unsafe public void Feed(byte[] bytes, int index, int count)
		{
			Ensure.InRange(bytes, nameof(bytes), index, nameof(index), count, nameof(count));

			fixed (byte* ptr = bytes) {
				Feed((IntPtr)ptr + index, count);
			}
		}

		public void Feed(byte[] bytes)
		{
			Ensure.ArgumentNotNull(bytes, nameof(bytes));

			Feed(bytes, 0, bytes.Length);
		}

		unsafe public void Feed(string text, Encoding encoding = null)
		{
			if (encoding == null) {
				encoding = Encoding.Default;
			}

			var bytes = encoding.GetBytes(text);
			Feed(bytes);
		}

		public void Feed(ArraySegment<byte> byteSegment)
		{
			Ensure.ArgumentNotDefault(byteSegment, nameof(byteSegment));
			
			Feed(byteSegment.Array, byteSegment.Count, byteSegment.Offset);
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_parser_finish(IntPtr parser);

		public Node Finish()
		{
			return Node.Create(cmark_parser_finish(pointer));
		}
	}
}

