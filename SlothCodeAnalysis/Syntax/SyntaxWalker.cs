using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax
{
    public abstract class SyntaxWalker
    {
        protected SyntaxWalkerDepth Depth { get; }

        protected SyntaxWalker(SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node)
        {
            Depth = depth;
        }

        public virtual void Visit(SyntaxNode node)
        {
            foreach (var child in node.ChildNodesAndTokens())
            {
                if (child.IsNode)
                {
                    if (Depth >= SyntaxWalkerDepth.Node)
                    {
                        Visit(child.AsNode());
                    }
                }
                else if (child.IsToken)
                {
                    if (this.Depth >= SyntaxWalkerDepth.Token)
                    {
                        VisitToken(child.AsToken());
                    }
                }
            }
        }

        protected virtual void VisitToken(SyntaxToken token)
        {
        }
    }
}
