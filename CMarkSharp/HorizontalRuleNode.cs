using System;

namespace CMarkSharp
{
	public class HorizontalRuleNode : Node
	{
		internal HorizontalRuleNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public HorizontalRuleNode()
			: base(NodeType.HorizontalRule)
		{
		}
	}
}

