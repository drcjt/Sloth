using SlothCodeAnalysis.Syntax.InternalSyntax;
using SlothCodeAnalysis.Text;

namespace SlothCodeAnalysis.Syntax
{
    public class SyntaxToken
    {
        internal GreenNode Node { get; }
        internal int Position { get; }
        internal int Index { get; }

        internal SyntaxToken(SyntaxNode parent, InternalSyntax.GreenNode token, int position, int index)
        {
            Parent = parent;
            Node = token;
            Position = position;
            Index = index;
        }

        internal SyntaxToken(GreenNode token)
        {
            Node = token;
        }

        public string Text => Node?.ToString();

        public SyntaxKind Kind { get { return Node.Kind; } }
        public SyntaxNode Parent { get; }

        public TextSpan Span
        {
            get
            {
                return Node != null ? new TextSpan(Position, Node.FullWidth) : default(TextSpan);
            }
        }

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