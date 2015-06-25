using System;

namespace CMarkSharp
{
	public class ParagraphNode : Node
	{
		internal ParagraphNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public ParagraphNode()
			: base(NodeType.Paragraph)
		{
		}
	}
}

