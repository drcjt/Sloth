namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class SyntaxTokenWithValue<T> : SyntaxToken
    {
        protected readonly string _textField;
        protected readonly T _valueField;

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value) : base(kind, text.Length)
        {
            _textField = text;
            _valueField = value;
        }

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value, string leadingTrivia, string trailingTrivia) : base(kind, text.Length, leadingTrivia, trailingTrivia)
        {
            _textField = text;
            _valueField = value;
        }

        public override string Text
        {
            get
            {
                return _textField;
            }
        }

        public override object Value
        {
            get
            {
                return _valueField;
            }
        }
    }
}
