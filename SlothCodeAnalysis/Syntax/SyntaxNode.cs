using System.Collections.Generic;

namespace SlothCodeAnalysis.Syntax
{
    public abstract class SyntaxNode : RedNode
    {
        internal SyntaxNode(InternalSyntax.GreenNode internalNode, RedNode parent, int position) : base(internalNode, parent, position)
        {
        }
    }
}
