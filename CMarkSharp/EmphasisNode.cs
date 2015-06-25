using System;

namespace CMarkSharp
{
	public class EmphasisNode : Node
	{
		internal EmphasisNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public EmphasisNode()
			: base(NodeType.Emphasis)
		{
		}
	}
}

