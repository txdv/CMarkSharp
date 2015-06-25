using System;

namespace CMarkSharp
{
	public class SoftBreakNode : Node
	{
		internal SoftBreakNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public SoftBreakNode()
			: base(NodeType.SoftBreak)
		{
		}
	}
}

