using System;

namespace CMarkSharp
{
	public class BlockQuoteNode : Node
	{
		internal BlockQuoteNode(IntPtr pointer)
			: base(pointer)
		{
		}

		public BlockQuoteNode()
			: base(NodeType.BlockQuote)
		{
		}
	}
}

