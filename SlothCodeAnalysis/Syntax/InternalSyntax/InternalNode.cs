namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    // TODO : Consider merging this and SyntaxNode
    internal abstract class InternalNode
    {
        public SyntaxKind Kind { get; private set; }
        public int FullWidth { get; private set; }

        protected InternalNode(SyntaxKind kind)
        {
            Kind = kind;
        }

        protected InternalNode(SyntaxKind kind, int fullWidth)
        {
            Kind = kind;
            FullWidth = fullWidth;
        }

        public virtual bool IsToken { get { return false; } }

        public virtual object GetValue() { return null; }

        public virtual string GetLeadingTrivia() { return string.Empty; }
        public virtual string GetTrailingTrivia() { return string.Empty; }
    }
}
