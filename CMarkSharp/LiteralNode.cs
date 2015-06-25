using System;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public abstract class LiteralNode : Node
	{
		internal LiteralNode(IntPtr pointer)
			: base(pointer)
		{
		}

		protected LiteralNode(NodeType nodeType)
			: base(nodeType)
		{
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		unsafe internal static extern sbyte* cmark_node_get_literal(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_literal(IntPtr node, string content);

		unsafe public string Literal {
			get {
				return new string(cmark_node_get_literal(pointer));
			}
			set {
				int r = cmark_node_set_literal(pointer, value);
				Ensure.Success(r);
			}
		}
	}
}

