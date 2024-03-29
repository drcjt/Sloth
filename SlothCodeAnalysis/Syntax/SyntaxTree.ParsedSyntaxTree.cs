﻿using SlothCodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax
{
    public partial class SyntaxTree
    {
        private class ParsedSyntaxTree : SyntaxTree
        {
            private SourceText _text;
            private readonly SyntaxNode _root;

            internal ParsedSyntaxTree(SourceText text, SyntaxNode root)
            {
                _text = text;
                _root = root;
            }

            public override SyntaxNode GetRoot()
            {
                return _root;
            }
        }
    }
}
