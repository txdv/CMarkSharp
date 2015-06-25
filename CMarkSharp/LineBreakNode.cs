using System;

namespace CMarkSharp
{
	public class LineBreakNode : Node
	{
		internal LineBreakNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public LineBreakNode()
			: base(NodeType.LineBreak)
		{
		}
	}
}

