using System;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public class CodeBlockNode : LiteralNode
	{
		internal CodeBlockNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public CodeBlockNode()
			: base(NodeType.CodeBlock)
		{
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		unsafe internal static extern sbyte* cmark_node_get_fence_info(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_fence_info(IntPtr node, string info);

		unsafe public string Info {
			get {
				return new string(cmark_node_get_fence_info(pointer));
			}
			set {
				int r = cmark_node_set_fence_info(pointer, value);
				Ensure.Success(r);
			}
		}
	}
}

