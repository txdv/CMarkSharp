using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public enum ListType {
		No,
		Bullet,
		Ordered
	};

	public enum DelimeterType {
		No,
		Period,
		Paren
	};

	public enum NodeType {
		/* Error status */
		None,

		/* Block */
		Document,
		BlockQuote,
		List,
		Item,
		CodeBlock,
		Html,
		Paragraph,
		Header,
		HRule,

		FirstBlock = Document,
		LastBlock  = HRule,

		/* Inline */
		Text,
		SoftBreak,
		LineBreak,
		Code,
		InlineHtml,
		Emphasis,
		Strong,
		Link,
		Image,

		FirstInline = Text,
		Lastinline  = Image,
	};

	public class Node : IDisposable
	{
		internal IntPtr pointer;

		internal Node(IntPtr pointer)
		{
			this.pointer = pointer;
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_new(NodeType type);

		public Node(NodeType nodeType)
			: this(cmark_node_new(nodeType))
		{
		}

		~Node()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true);
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern void cmark_node_free(IntPtr node);

		protected void Dispose(bool disposing)
		{
			if (pointer != IntPtr.Zero) {
				cmark_node_free(pointer);
				pointer = IntPtr.Zero;
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_next(IntPtr node);

		public Node Next {
			get {
				return new Node(cmark_node_next(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_previous(IntPtr node);

		public Node Previous {
			get {
				return new Node(cmark_node_previous(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_parent(IntPtr node);

		public Node Parent {
			get {
				return new Node(cmark_node_previous(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_first_child(IntPtr node);

		public Node FirstChild {
			get {
				return new Node(cmark_node_first_child(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_last_child(IntPtr node);

		public Node LastChild {
			get {
				return new Node(cmark_node_first_child(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_get_user_data(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_user_data(IntPtr node, IntPtr data);

		public IntPtr Data {
			get {
				return cmark_node_get_user_data(pointer);
			}
			set {
				int r = cmark_node_set_user_data(pointer, value);
				Ensure.Success(r);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern NodeType cmark_node_get_type(IntPtr node);

		public NodeType NodeType {
			get {
				return cmark_node_get_type(pointer);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		unsafe internal static extern sbyte* cmark_node_get_type_string(IntPtr node);

		unsafe public string NodeTypeString {
			get {
				return new string(cmark_node_get_type_string(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_get_literal(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_literal(IntPtr node, IntPtr content);

		// TODO: This thing should return a string, investigate what it really is
		public IntPtr Literal {
			get {
				return cmark_node_get_literal(pointer);
			}
			set {
				int r = cmark_node_set_literal(pointer, value);
				Ensure.Success(r);
			}
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

		#region List

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

		#endregion

		#region CodeBlock

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_get_fence_info(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_fence_info(IntPtr node, IntPtr info);

		// TODO: this should be a string, investigate

		public IntPtr Info {
			get {
				return cmark_node_get_fence_info(pointer);
			}
			set {
				int r = cmark_node_set_fence_info(pointer, value);
				Ensure.Success(r);
			}
		}

		#endregion

		#region Url

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_get_url(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_url(IntPtr node, IntPtr info);

		// TODO: This should be a string, investigate

		public IntPtr Url {
			get {
				return cmark_node_get_url(pointer);
			}
			set {
				int r = cmark_node_set_url(pointer, value);
				Ensure.Success(r);
			}
		}

		#endregion

		#region Image

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_get_title(IntPtr node);

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_set_title(IntPtr node, IntPtr info);

		// TODO: This should be a string, investigate

		public IntPtr Title {
			get {
				return cmark_node_get_title(pointer);
			}
			set {
				int r = cmark_node_set_title(pointer, value);
				Ensure.Success(r);
			}
		}

		#endregion

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_get_start_line(IntPtr node);

		public int StartLine {
			get {
				return cmark_node_get_start_line(pointer);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_get_end_line(IntPtr node);

		public int EndLine {
			get {
				return cmark_node_get_end_line(pointer);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_get_start_column(IntPtr node);

		public int StartColumn {
			get {
				return cmark_node_get_start_column(pointer);
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_get_end_column(IntPtr node);

		public int EndColumn {
			get {
				return cmark_node_get_end_column(pointer);
			}
		}

		#region Tree Manipulation

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern void cmark_node_unlink(IntPtr node);

		public void Unlink()
		{
			cmark_node_unlink(pointer);
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_insert_before(IntPtr node, IntPtr sibling);

		public void InsertBefore(Node sibling)
		{
			int r = cmark_node_insert_before(pointer, sibling.pointer);
			Ensure.Success(r);
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_insert_after(IntPtr node, IntPtr sibling);

		public void InsertAfter(Node sibling)
		{
			int r = cmark_node_insert_after(pointer, sibling.pointer);
			Ensure.Success(r);
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_node_prepend_child(IntPtr node, IntPtr child);

		public void PrependChild(Node child)
		{
			int r = cmark_node_prepend_child(pointer, child.pointer);
			Ensure.Success(r);
		}

		// TODO: type for root node?

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern int cmark_consolidate_text_nodes(IntPtr root);

		public void ConsolidateTextNodes()
		{
			cmark_consolidate_text_nodes(pointer);
		}

		#endregion
	}
}

