namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int fullWidth) : base(kind, fullWidth)
        {
        }

        public SyntaxToken(SyntaxKind kind, int fullWidth, string leadingTrivia, string trailingTrivia) : base(kind, fullWidth)
        {
            _leadingTrivia = leadingTrivia ?? null;
            _trailingTrivia = trailingTrivia ?? null;
        }

        public SyntaxToken(SyntaxKind kind) : base(kind, SyntaxFacts.GetText(kind).Length)
        {
        }

        public SyntaxToken(SyntaxKind kind, string leadingTrivia, string trailingTrivia) : base(kind, SyntaxFacts.GetText(kind).Length)
        {
            _leadingTrivia = leadingTrivia ?? null;
            _trailingTrivia = trailingTrivia ?? null;
        }

        public override bool IsToken { get { return true; } }

        public virtual string Text
        {
            get { return SyntaxFacts.GetText(Kind); }
        }

        public override string ToString()
        {
            return Text;
        }

        public virtual object Value
        {
            get
            {
                return Text;
            }
        }

        public override object GetValue()
        {
            return Value;
        }

        protected readonly string _leadingTrivia;
        protected readonly string _trailingTrivia;

        public override string GetLeadingTrivia()
        {
            return _leadingTrivia;
        }

        public override string GetTrailingTrivia()
        {
            return _trailingTrivia;
        }

        internal static SyntaxToken CreateMissing(SyntaxKind kind, string leading, string trailing)
        {
            return new MissingToken(kind, leading, trailing);
        }
    }
}
