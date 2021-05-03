using SlothCodeAnalysis.Diagnostic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

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

        internal SyntaxTree _syntaxTree;
        internal SyntaxTree SyntaxTree
        {
            get
            {
                var result = this._syntaxTree ?? ComputeSyntaxTree(this);
                Debug.Assert(result != null);
                return result;
            }
        }

        private static SyntaxTree ComputeSyntaxTree(SyntaxNode node)
        {
            // TODO: change to use ArrayBuilder??
            List<SyntaxNode> nodes = new List<SyntaxNode>();
            SyntaxTree tree = null;

            // Find the nearest parent with a non-null syntax tree
            while (true)
            {
                tree = node._syntaxTree;
                if (tree != null)
                {
                    break;
                }

                var parent = node.Parent;
                if (parent == null)
                {
                    // set the tree on the root node atomically
                    Interlocked.CompareExchange(ref node._syntaxTree, SyntaxTree.CreateWithoutClone(node), null);
                    tree = node._syntaxTree;
                    break;
                }

                tree = parent._syntaxTree;
                if (tree != null)
                {
                    node._syntaxTree = tree;
                    break;
                }

                nodes.Add(node);
                node = parent;
            }

            // Propagate the syntax tree downwards if necessary
            if (nodes != null)
            {
                Debug.Assert(tree != null);

                foreach (var n in nodes)
                {
                    var existingTree = n._syntaxTree;
                    if (existingTree != null)
                    {
                        Debug.Assert(existingTree == tree, "how could this node belong to a different tree?");

                        // yield the race
                        break;
                    }
                    n._syntaxTree = tree;
                }
            }

            return tree;
        }

        internal SourceLocation Location
        {
            get
            {
                return new SourceLocation(this);
            }
        }

        public override string ToString()
        {
            return GreenNode.ToString();
        }

        internal new SyntaxNode Parent
        {
            get
            {
                return (SyntaxNode)base.Parent;
            }
        }
    }
}
