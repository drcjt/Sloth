using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax
{
    public class ChildSyntaxList : IReadOnlyList<SyntaxNodeOrToken>
    {
        private readonly SyntaxNode _node;
        private readonly int _count;

        internal ChildSyntaxList(SyntaxNode node)
        {
            _node = node;
            _count = CountNodes(node.GreenNode);
        }

        internal static int CountNodes(InternalSyntax.GreenNode green)
        {
            var n = 0;
            var s = green.SlotCount;

            for (int i = 0; i < s; i++)
            {
                var child = green.GetSlot(i);
                if (child != null)
                {
                    if (!child.IsList)
                    {
                        n++;
                    }
                    else
                    {
                        n += child.SlotCount;
                    }
                }
            }

            return n;
        }

        public SyntaxNodeOrToken this[int index]
        {
            get
            {
                if (index < _count)
                {
                    return ItemInternal(_node, index);
                }

                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        internal static SyntaxNodeOrToken ItemInternal(SyntaxNode node, int index)
        {
            InternalSyntax.GreenNode greenChild;
            var green = node.GreenNode;
            var idx = index;
            var slotIndex = 0;
            var position = node.Position;

            while (true)
            {
                greenChild = green.GetSlot(slotIndex);
                if (greenChild != null)
                {
                    int currentOccupancy = Occupancy(greenChild);
                    if (idx < currentOccupancy)
                    {
                        break;
                    }

                    idx -= currentOccupancy;
                    position += greenChild.FullWidth;
                }

                slotIndex++;
            }

            // get node that represents this slot
            var red = node.GetNodeSlot(slotIndex);
            if (!greenChild.IsList)
            {
                // this is a single node or token
                // if it is a node, we are done
                // otherwise will have to make a token with current gChild and position
                if (red != null)
                {
                    return red;
                }
            }
            else if (red != null)
            {
                // it is a red list of nodes (separated or not), most common case
                var redChild = red.GetNodeSlot(idx);
                if (redChild != null)
                {
                    // this is our node
                    return redChild;
                }
            }

            return new SyntaxNodeOrToken(node, greenChild, position, index);
        }

        private static int Occupancy(InternalSyntax.GreenNode green)
        {
            return green.IsList ? green.SlotCount : 1;
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public IEnumerator<SyntaxNodeOrToken> GetEnumerator()
        {
            return new EnumeratorImpl(_node, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EnumeratorImpl(_node, _count);
        }

        public struct Enumerator
        {
            private SyntaxNode _node;
            private int _count;
            private int _childIndex;

            internal Enumerator(SyntaxNode node, int count)
            {
                _node = node;
                _count = count;
                _childIndex = -1;
            }

            // PERF: Initialize an Enumerator directly from a SyntaxNode without going
            // via ChildNodesAndTokens. This saves constructing an intermediate ChildSyntaxList
            internal void InitializeFrom(SyntaxNode node)
            {
                _node = node;
                _count = CountNodes(node.GreenNode);
                _childIndex = -1;
            }

            /// <summary>Advances the enumerator to the next element of the <see cref="ChildSyntaxList" />.</summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                var newIndex = _childIndex + 1;
                if (newIndex < _count)
                {
                    _childIndex = newIndex;
                    return true;
                }

                return false;
            }

            /// <summary>Gets the element at the current position of the enumerator.</summary>
            /// <returns>The element in the <see cref="ChildSyntaxList" /> at the current position of the enumerator.</returns>
            public SyntaxNodeOrToken Current
            {
                get
                {
                    return ItemInternal(_node, _childIndex);
                }
            }

            /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
            public void Reset()
            {
                _childIndex = -1;
            }

            /*
            internal bool TryMoveNextAndGetCurrent(ref SyntaxNodeOrToken current)
            {
                if (!MoveNext())
                {
                    return false;
                }

                current = ItemInternal(_node, _childIndex);
                return true;
            }
            
            internal SyntaxNode TryMoveNextAndGetCurrentAsNode()
            {
                while (MoveNext())
                {
                    var nodeValue = ItemInternalAsNode(_node, _childIndex);
                    if (nodeValue != null)
                    {
                        return nodeValue;
                    }
                }

                return null;
            }
            */
        }

        private class EnumeratorImpl : IEnumerator<SyntaxNodeOrToken>
        {
            private Enumerator _enumerator;

            internal EnumeratorImpl(SyntaxNode node, int count)
            {
                _enumerator = new Enumerator(node, count);
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            ///   </returns>
            public SyntaxNodeOrToken Current
            {
                get { return _enumerator.Current; }
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            ///   </returns>
            object IEnumerator.Current
            {
                get { return _enumerator.Current; }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                _enumerator.Reset();
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            { }
        }
    }
}
