using SlothCodeAnalysis.Syntax;
using SlothCodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Diagnostic
{
    class SourceLocation
    {
        private readonly SyntaxTree _syntaxTree;
        private readonly TextSpan _span;

        public SourceLocation(SyntaxTree syntaxTree, TextSpan span)
        {
            _syntaxTree = syntaxTree;
            _span = span;
        }

        public SourceLocation(SyntaxNode node) : this(node.SyntaxTree, node.Span)
        {
        }

        public virtual TextSpan SourceSpan { get { return _span; } }

        public override string ToString()
        {
            string result = "SourceFile";
            result += "(" + this.SourceSpan + ")";

            return result;
        }
    }
}
