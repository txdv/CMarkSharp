using System;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public class ListNode : Node
	{
		internal ListNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public ListNode()
			: base(NodeType.List)
		{
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern ListType cmark_node_get_list_type(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_list_type(IntPtr node, ListType type);

		public ListType ListType {
			get {
				return cmark_node_get_list_type(pointer);
			}
			set {
				int r = cmark_node_set_list_type(pointer, value);
				Ensure.Success(r);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern DelimeterType cmark_node_get_list_delim(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_list_delim(IntPtr node, DelimeterType type);

		public DelimeterType DelimeterType {
			get {
				return cmark_node_get_list_delim(pointer);
			}
			set {
				int r = cmark_node_set_list_delim(pointer, value);
				Ensure.Success(r);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_get_list_start(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_list_start(IntPtr node, int start);

		public int Start {
			get {
				return cmark_node_get_list_start(pointer);
			}
			set {
				int r = cmark_node_set_list_start(pointer, value);
				Ensure.Success(r);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_get_list_tight(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_list_tight(IntPtr node, int start);

		public int Tightness {
			get {
				return cmark_node_get_list_tight(pointer);
			}
			set {
				int r = cmark_node_set_list_tight(pointer, value);
				Ensure.Success(r);
			}
		}
	}
}

