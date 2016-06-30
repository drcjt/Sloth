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
            for (int i = 0; i < _children.Length; i++)
            {
                AdjustWidth(_children[i]);
            }
            SlotCount = _children.Length;
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

        internal override GreenNode GetSlot(int index)
        {
            return _children[index];
        }

        internal int GetCount()
        {
            return _children.Length;
        }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.SyntaxList(this, parent, position);
        }
    }

    // Immutable list of typed syntax nodes
    internal class SyntaxList<TNode> where TNode : SyntaxNode
    {
        private readonly SyntaxList _list;

        internal SyntaxList(SyntaxList list)
        {
            _list = list;
        }

        internal SyntaxList List
        {
            get
            {
                return _list;
            }
        }

        public TNode this[int index]
        {
            get
            {
                return (TNode)_list[index];
            }
        }
    }
}
