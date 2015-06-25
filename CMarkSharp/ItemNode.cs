using System;

namespace CMarkSharp
{
	public class ItemNode : Node
	{
		internal ItemNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public ItemNode()
			: base(NodeType.Item)
		{
		}
	}
}

