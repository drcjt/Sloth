namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class SyntaxIdentifier : SyntaxToken
    {
        protected readonly string TextField;

        internal SyntaxIdentifier(SyntaxKind kind, string text) : base(kind, text.Length)
        {
            TextField = text;
        }

        internal SyntaxIdentifier(SyntaxKind kind, string text, string leadingTrivia, string trailingTrivia) : base(kind, text.Length, leadingTrivia, trailingTrivia)
        {
            TextField = text;
        }

        public override string Text
        {
            get
            {
                return TextField;
            }
        }
    }
}
