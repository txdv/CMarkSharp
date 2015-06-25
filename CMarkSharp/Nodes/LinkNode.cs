using System;
using System.Runtime.InteropServices;

namespace CMarkSharp
{
	public class LinkNode : ResourceNode
	{
		internal LinkNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public LinkNode()
			: base(NodeType.Link)
		{
		}
	}
}

