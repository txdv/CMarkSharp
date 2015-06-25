using System;

namespace CMarkSharp
{
	public class CodeNode : LiteralNode
	{
		internal CodeNode(IntPtr pointer)
			: base(pointer)
		{
		}

		protected CodeNode()
			: base(NodeType.Code)
		{
		}
	}
}

