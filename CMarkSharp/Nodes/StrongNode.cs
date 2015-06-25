using System;

namespace CMarkSharp
{
	public class StrongNode : Node
	{
		internal StrongNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public StrongNode()
			: base(NodeType.Strong)
		{
		}
	}
}

