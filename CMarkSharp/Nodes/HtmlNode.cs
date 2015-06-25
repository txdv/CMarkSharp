using System;

namespace CMarkSharp
{
	public class HtmlNode : LiteralNode
	{
		internal HtmlNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public HtmlNode()
			: base(NodeType.Html)
		{
		}
	}
}

