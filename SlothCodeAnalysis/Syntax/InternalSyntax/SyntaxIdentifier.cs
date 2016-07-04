namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class SyntaxIdentifier : SyntaxToken
    {
        protected readonly string _textField;

        internal SyntaxIdentifier(SyntaxKind kind, string text) : base(kind, text.Length)
        {
            _textField = text;
        }

        internal SyntaxIdentifier(SyntaxKind kind, string text, string leadingTrivia, string trailingTrivia) : base(kind, text.Length, leadingTrivia, trailingTrivia)
        {
            _textField = text;
        }

        public override string Text
        {
            get
            {
                return _textField;
            }
        }
    }
}
