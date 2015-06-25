using System;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public abstract class ResourceNode : Node
	{
		internal ResourceNode(IntPtr pointer)
			: base(pointer)
		{
		}

		protected ResourceNode(NodeType nodeType)
			: base(nodeType)
		{
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		unsafe internal static extern sbyte* cmark_node_get_url(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_url(IntPtr node, string info);

		unsafe public string Url {
			get {
				return new string(cmark_node_get_url(pointer));
			}
			set {
				int r = cmark_node_set_url(pointer, value);
				Ensure.Success(r);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		unsafe internal static extern sbyte* cmark_node_get_title(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_title(IntPtr node, string info);

		unsafe public string Title {
			get {
				return new string(cmark_node_get_title(pointer));
			}
			set {
				int r = cmark_node_set_title(pointer, value);
				Ensure.Success(r);
			}
		}
	}
}

