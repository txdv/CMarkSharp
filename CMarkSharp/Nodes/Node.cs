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
		HorizontalRule,

		FirstBlock = Document,
		LastBlock  = HorizontalRule,

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
		LastInline  = Image,
	};

	public abstract class Node : IDisposable
	{
		internal static Node Create(IntPtr pointer)
		{
			var nodeType = cmark_node_get_type(pointer);
			switch (nodeType) {
			case NodeType.BlockQuote:
				return new BlockQuoteNode(pointer);
			case NodeType.CodeBlock:
				return new CodeBlockNode(pointer);
			case NodeType.Code:
				return new CodeNode(pointer);
			case NodeType.Document:
				return new DocumentNode(pointer);
			case NodeType.Emphasis:
				return new EmphasisNode(pointer);
			case NodeType.Header:
				return new HeaderNode(pointer);
			case NodeType.HorizontalRule:
				return new HorizontalRuleNode(pointer);
			case NodeType.Image:
				return new ImageNode(pointer);
			case NodeType.LineBreak:
				return new LineBreakNode(pointer);
			case NodeType.Link:
				return new LinkNode(pointer);
			case NodeType.List:
				return new ListNode(pointer);
			case NodeType.InlineHtml:
				return new InlineHtmlNode(pointer);
			case NodeType.Item:
				return new ItemNode(pointer);
			case NodeType.Paragraph:
				return new ParagraphNode(pointer);
			case NodeType.Html:
				return new HtmlNode(pointer);
			case NodeType.SoftBreak:
				return new SoftBreakNode(pointer);
			case NodeType.Strong:
				return new StrongNode(pointer);
			case NodeType.Text:
				return new TextNode(pointer);
			default:
				throw new Exception(string.Format("NodeType {0} not supported", nodeType));
			}
		}

		internal IntPtr pointer;

		internal Node(IntPtr pointer)
		{
			this.pointer = pointer;
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_new(NodeType type);

		protected Node(NodeType nodeType)
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
				return Node.Create(cmark_node_next(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_previous(IntPtr node);

		public Node Previous {
			get {
				return Node.Create(cmark_node_previous(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_parent(IntPtr node);

		public Node Parent {
			get {
				return Node.Create(cmark_node_previous(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_first_child(IntPtr node);

		public Node FirstChild {
			get {
				return Node.Create(cmark_node_first_child(pointer));
			}
		}

		[DllImport("cmark", CallingConvention=CallingConvention.Cdecl)]
		internal static extern IntPtr cmark_node_last_child(IntPtr node);

		public Node LastChild {
			get {
				return Node.Create(cmark_node_first_child(pointer));
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

