using System;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public enum cmark_event_type {
		CMARK_EVENT_NONE,
		CMARK_EVENT_DONE,
		CMARK_EVENT_ENTER,
		CMARK_EVENT_EXIT
	};

	public class Iterator
	{
		IntPtr pointer;

		private Iterator(IntPtr pointer)
		{
			this.pointer = pointer;
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern cmark_event_type cmark_iter_next(IntPtr iter);

		public cmark_event_type Next() 
		{
			return cmark_iter_next(pointer);
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_iter_get_node(IntPtr iter);

		public Node Node {
			get {
				return new Node(cmark_iter_get_node(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern cmark_event_type cmark_iter_get_event_type(IntPtr iter);

		public cmark_event_type EventType
		{
			get {
				return cmark_iter_get_event_type(pointer);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_iter_get_root(IntPtr iter);

		public Node Root
		{
			get {
				return new Node(cmark_iter_get_root(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern void cmark_iter_reset(IntPtr iter, IntPtr cmark_node, cmark_event_type event_type);

		public void Reset(Node currentNode, cmark_event_type eventType)
		{
			cmark_iter_reset(pointer, currentNode.pointer, eventType);
		}
	}
}

