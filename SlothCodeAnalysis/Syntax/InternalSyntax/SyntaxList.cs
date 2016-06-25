using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    // Immutable list of syntax nodes
    internal class SyntaxList : SyntaxNode
    {
        internal readonly SyntaxNode[] _children;

        internal SyntaxList() : base(SyntaxKind.List)
        {
        }

        internal SyntaxList(SyntaxNode[] nodes) : base(SyntaxKind.List)
        {
            _children = nodes;
        }

        internal static SyntaxNode List(SyntaxNode[] nodes)
        {
            return new SyntaxList(nodes);
        }

        public SyntaxNode this[int index]
        {
            get
            {
                return _children[index];
            }
        }
    }

    // Immutable list of typed syntax nodes
    internal class SyntaxList<TNode> : SyntaxNode where TNode : SyntaxNode
    {
        internal readonly TNode[] _children;

        internal SyntaxList() : base(SyntaxKind.List)
        {
        }

        internal SyntaxList(TNode[] nodes) : base(SyntaxKind.List)
        {
            _children = nodes;
        }

        internal static SyntaxNode List(SyntaxNode[] nodes)
        {
            return new SyntaxList(nodes);
        }

        public SyntaxNode this[int index]
        {
            get
            {
                return _children[index];
            }
        }
    }
}
