﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax
{
    public class SyntaxList : SyntaxNode
    {
        private readonly SyntaxNode[] _children;

        internal SyntaxList(SlothCodeAnalysis.Syntax.InternalSyntax.SyntaxList green, RedNode parent, int position) : base(green, parent, position)
        {
            _children = new SyntaxNode[green.GetCount()];
        }

        public SyntaxNode this[int index]
        {
            get
            {
                return GetRedElement(ref _children[index], index);
            }
        }

        internal override SyntaxNode GetNodeSlot(int index)
        {
            return this.GetRedElement(ref _children[index], index);
        }

        public int Count
        {
            get
            {
                return ((InternalSyntax.SyntaxList)GreenNode)._children.Length;
            }
        }
    }

    public class SyntaxList<TNode> where TNode : SyntaxNode
    {
        private readonly SyntaxList _list;

        internal SyntaxList(SyntaxList list)
        {
            _list = list;
        }

        public TNode this[int index]
        {
            get
            {
                return (TNode)_list[index];
            }
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }
    }
}
