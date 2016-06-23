using SlothCodeAnalysis.Syntax.InternalSyntax;

namespace SlothCodeAnalysis.Syntax
{
    public class SyntaxToken
    {
        internal InternalNode Node { get; }

        internal SyntaxToken(InternalNode token)
        {
            Node = token;
        }

        public string Text => Node?.ToString();

        public SyntaxKind Kind { get { return Node.Kind; } }
        public SyntaxNode Parent { get; }

        public object Value => Node?.GetValue();

        public override string ToString()
        {
            return Text;
        }

        public string LeadingTrivia
        {
            get
            {
                return Node?.GetLeadingTrivia();
            }
        }

        public string TrailingTrivia
        {
            get
            {
                return Node?.GetTrailingTrivia();
            }
        }
    }
}