namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class SyntaxTokenWithValue<T> : SyntaxToken
    {
        protected readonly string TextField;
        protected readonly T ValueField;

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value) : base(kind, text.Length)
        {
            TextField = text;
            ValueField = value;
        }

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value, string leadingTrivia, string trailingTrivia) : base(kind, text.Length, leadingTrivia, trailingTrivia)
        {
            TextField = text;
            ValueField = value;
        }

        public override string Text
        {
            get
            {
                return TextField;
            }
        }

        public override object Value
        {
            get
            {
                return ValueField;
            }
        }
    }
}
