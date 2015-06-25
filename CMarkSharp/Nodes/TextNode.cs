using System;

namespace CMarkSharp
{
	public class TextNode : LiteralNode
	{
		internal TextNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public TextNode()
			: base(NodeType.Text)
		{
		}
	}
}

