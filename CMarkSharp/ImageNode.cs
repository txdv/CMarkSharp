using System;

namespace CMarkSharp
{
	public class ImageNode : ResourceNode
	{
		internal ImageNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public ImageNode()
			: base(NodeType.Image)
		{
		}
	}
}

