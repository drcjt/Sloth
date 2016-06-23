namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal static class SyntaxFactory
    {
        internal static SyntaxToken Token(SyntaxKind kind)
        {
            return new SyntaxToken(kind, SyntaxKindFacts.GetText(kind).Length);
        }

        internal static SyntaxToken Token(string leading, SyntaxKind kind, string trailing)
        {
            return new SyntaxToken(kind, leading, trailing);
        }

        internal static SyntaxToken Identifier(string text)
        {
            return new SyntaxIdentifier(SyntaxKind.IdentifierToken, text);
        }

        internal static SyntaxToken Identifier(string leading, string text, string trailing)
        {
            return new SyntaxIdentifier(SyntaxKind.IdentifierToken, text, leading, trailing);
        }

        internal static SyntaxToken Literal(string text, int value)
        {
            return WithValue<int>(SyntaxKind.NumericLiteralToken, text, value);
        }

        internal static SyntaxToken Literal(string leading, string text, int value, string trailing)
        {
            return WithValue<int>(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
        }

        internal static SyntaxToken Literal(string text)
        {
            return WithValue<string>(SyntaxKind.StringLiteralToken, text, text);
        }

        internal static SyntaxToken Literal(string leading, string text, string trailing)
        {
            return WithValue<string>(SyntaxKind.StringLiteralToken, leading, text, text, trailing);
        }

        internal static SyntaxToken WithValue<T>(SyntaxKind kind, string text, T value)
        {
            return new SyntaxTokenWithValue<T>(kind, text, value);
        }

        internal static SyntaxToken WithValue<T>(SyntaxKind kind, string leading, string text, T value, string trailing)
        {
            return new SyntaxTokenWithValue<T>(kind, text, value, leading, trailing);
        }
    }
}
