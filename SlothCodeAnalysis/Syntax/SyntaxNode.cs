using System.Collections.Generic;

namespace SlothCodeAnalysis.Syntax
{
    public abstract class SyntaxNode
    {
        private readonly SyntaxNode _parent;
        internal SyntaxTree _syntaxTree;
        internal SyntaxTree[] _childNodes;

        protected abstract string KindText { get; }

        public SyntaxNode Parent
        {
            get
            {
                return _parent;
            }
        }

        public bool Contains(SyntaxNode node)
        {
            if (node == null)
            {
                return false;
            }

            while (node != null)
            {
                if (node == this)
                {
                    return true;
                }

                if (node.Parent != null)
                {
                    node = node.Parent;
                }
                else
                {
                    node = null;
                }
            }

            return false;
        }

        public IEnumerable<SyntaxNode> ChildNodes()
        {
            return null;
        }

        public IEnumerable<SyntaxToken> ChildTokens()
        {
            return null;
        }
    }
}
