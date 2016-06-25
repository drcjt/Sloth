using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    // TODO - might be able to avoid duplication between these two classes
    // In Roslyn the typed SyntaxListBuilder uses an untyped SyntaxListBuilder internally and does
    // something clever to extract the typed SyntaxList<TNode> from it.

    internal class SyntaxListBuilder
    {
        private SyntaxNode[] _nodes;
        public int Count { get; private set; }

        public SyntaxListBuilder()
        {
            _nodes = new SyntaxNode[10];
        }

        public void Add(SyntaxNode node)
        {
            if (node != null)
            {
                EnsureAdditionalCapacity(1);
                _nodes[Count++] = node;
            }
        }

        private void EnsureAdditionalCapacity(int additionalCount)
        {
            int currentSize = _nodes.Length;
            int requiredSize = Count + additionalCount;

            if (requiredSize >= currentSize)
            {
                int newSize = Math.Max(requiredSize, currentSize * 2);

                Array.Resize(ref _nodes, newSize);
            }
        }

        public SyntaxList ToListNode()
        {
            // Copy nodes to ensure created syntax list is immutable and not sharing the nodes
            var tmp = new SyntaxNode[Count];
            Array.Copy(_nodes, tmp, Count);
            return new SyntaxList(tmp);
        }
    }

    internal class SyntaxListBuilder<TNode> where TNode : SyntaxNode
    {
        private TNode[] _nodes;
        public int Count { get; private set; }

        public SyntaxListBuilder()
        {
            _nodes = new TNode[10];
        }

        public void Add(TNode node)
        {
            if (node != null)
            {
                EnsureAdditionalCapacity(1);
                _nodes[Count++] = node;
            }
        }

        private void EnsureAdditionalCapacity(int additionalCount)
        {
            int currentSize = _nodes.Length;
            int requiredSize = Count + additionalCount;

            if (requiredSize >= currentSize)
            {
                int newSize = Math.Max(requiredSize, currentSize * 2);

                Array.Resize(ref _nodes, newSize);
            }
        }

        public SyntaxList<TNode> ToListNode()
        {
            // Copy nodes to ensure created syntax list is immutable and not sharing the nodes
            var tmp = new TNode[Count];
            Array.Copy(_nodes, tmp, Count);
            return new SyntaxList<TNode>(tmp);
        }
    }
}
