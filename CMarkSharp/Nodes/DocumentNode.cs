using System;

namespace CMarkSharp
{
	public class DocumentNode : Node
	{
		internal DocumentNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public DocumentNode()
			: base(NodeType.Document)
		{
		}
	}
}

