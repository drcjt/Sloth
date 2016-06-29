using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax
{
    public abstract class RedNode
    {
        private readonly RedNode _parent;
        internal InternalSyntax.GreenNode GreenNode { get; }
        internal int Position { get; }

        internal RedNode(InternalSyntax.GreenNode greenNode, RedNode parent, int position)
        {
            GreenNode = greenNode;
            _parent = parent;
            Position = position;
        }

        public RedNode Parent
        {
            get
            {
                return _parent;
            }
        }

        internal T GetRed<T>(ref T field, int slot) where T : RedNode
        {
            var result = field;

            if (result == null)
            {
                var green = GreenNode.GetSlot(slot);
                if (green != null)
                {
                    Interlocked.CompareExchange(ref field, (T)green.CreateRedNode(this, GetChildPosition(slot)), null);
                    result = field;
                }
            }

            return result;
        }

        internal RedNode GetRedElement(ref RedNode element, int slot)
        {
            var result = element;

            if (result == null)
            {
                var green = GreenNode.GetSlot(slot);
                // passing list's parent
                Interlocked.CompareExchange(ref element, green.CreateRedNode(Parent, GetChildPosition(slot)), null);
                result = element;
            }

            return result;
        }

        /// <summary>
        /// Calculate offset of a child at a given position
        /// </summary>
        /// <param name="index">index of child to get offset of</param>
        /// <returns>offset of child</returns>
        internal virtual int GetChildPosition(int index)
        {
            int offset = 0;
            var green = GreenNode;
            while (index > 0)
            {
                index--;
                var greenChild = green.GetSlot(index);
                if (greenChild != null)
                {
                    offset += greenChild.FullWidth;
                }
            }

            return this.Position + offset;
        }
    }
}
