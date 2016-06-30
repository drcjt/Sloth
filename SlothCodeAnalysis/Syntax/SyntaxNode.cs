using System.Collections.Generic;

namespace SlothCodeAnalysis.Syntax
{
    public abstract class SyntaxNode : RedNode
    {
        internal SyntaxNode(InternalSyntax.GreenNode internalNode, RedNode parent, int position) : base(internalNode, parent, position)
        {
        }

        public ChildSyntaxList ChildNodesAndTokens()
        {
            return new ChildSyntaxList(this);
        }

        /// <summary>
        /// Gets node at given node index. 
        /// This WILL force node creation if node has not yet been created.
        /// </summary>
        internal virtual SyntaxNode GetNodeSlot(int slot)
        {
            return null;
        }

        public override string ToString()
        {
            return GreenNode.ToString();
        }
    }
}
