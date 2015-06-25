using System;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public class HeaderNode : Node
	{
		internal HeaderNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public HeaderNode()
			: base(NodeType.List)
		{
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_get_header_level(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_header_level(IntPtr node, int level);

		public int HeaderLevel {
			get {
				return cmark_node_get_header_level(pointer);
			}
			set {
				int r = cmark_node_set_header_level(pointer, value);
				Ensure.Success(r);
			}
		}
	}
}

