using System;

namespace CMarkSharp
{
	public class InlineHtmlNode : LiteralNode
	{
		internal InlineHtmlNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public InlineHtmlNode()
			: base(NodeType.InlineHtml)
		{
		}
	}
}

