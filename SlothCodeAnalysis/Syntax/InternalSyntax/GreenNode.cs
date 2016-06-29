namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    // TODO : Consider merging this and SyntaxNode
    internal abstract class GreenNode
    {
        public SyntaxKind Kind { get; private set; }
        public int FullWidth { get; private set; }

        protected GreenNode(SyntaxKind kind)
        {
            Kind = kind;
        }

        protected GreenNode(SyntaxKind kind, int fullWidth)
        {
            Kind = kind;
            FullWidth = fullWidth;
        }

        protected void AdjustWidth(GreenNode node)
        {
            FullWidth += node.FullWidth;
        }

        public virtual bool IsToken { get { return false; } }

        public virtual object GetValue() { return null; }

        public virtual string GetLeadingTrivia() { return string.Empty; }
        public virtual string GetTrailingTrivia() { return string.Empty; }

        public RedNode CreateRedNode()
        {
            return CreateRedNode(null, 0);
        }

        internal abstract RedNode CreateRedNode(RedNode parent, int position);

        internal abstract GreenNode GetSlot(int index);
    }
}
