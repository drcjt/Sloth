using System;

namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal static class SyntaxFactory
    {
        internal static SyntaxToken Token(SyntaxKind kind)
        {
            return new SyntaxToken(kind, SyntaxFacts.GetText(kind).Length);
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

        public static EmptyStatementSyntax EmptyStatement(SyntaxToken semicolonToken)
        {
            return new EmptyStatementSyntax(SyntaxKind.EmptyStatement, semicolonToken);
        }

        internal static SyntaxToken MissingToken(SyntaxKind kind)
        {
            return SyntaxToken.CreateMissing(kind, string.Empty, string.Empty);
        }

        public static CompilationUnitSyntax CompilationUnit(SyntaxList<StatementSyntax> statements, SyntaxToken endOfFileToken)
        {
            return new CompilationUnitSyntax(SyntaxKind.CompilationUnit, statements, endOfFileToken);
        }

        public static PrintStatementSyntax PrintStatement(SyntaxToken printKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        {
            return new PrintStatementSyntax(SyntaxKind.PrintStatement, printKeyword, expression, semicolonToken);
        }

        public static VariableDeclarationSyntax VariableDeclaration(SyntaxToken varKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression, SyntaxToken semicolonToken)
        {
            return new VariableDeclarationSyntax(SyntaxKind.VariableDeclaration, varKeyword, identifier, equalsToken, expression, semicolonToken);
        }

        public static ReadIntSyntax ReadInt(SyntaxToken readIntKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
        {
            return new ReadIntSyntax(SyntaxKind.ReadIntKeyword, readIntKeyword, identifier, semicolonToken);
        }

        public static ForStatementSyntax For(SyntaxToken forKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax lower, SyntaxToken toKeyword, ExpressionSyntax upper, SyntaxToken doKeyword, SyntaxList<StatementSyntax> body, SyntaxToken endKeyword, SyntaxToken semicolonToken)
        {
            return new ForStatementSyntax(SyntaxKind.ForKeyword, forKeyword, identifier, equalsToken, lower, toKeyword, upper, doKeyword, body, endKeyword, semicolonToken);
        }

        public static AssignmentSyntax Assignment(SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression, SyntaxToken semicolonToken)
        {
            return new AssignmentSyntax(SyntaxKind.AssignmentStatement, identifier, equalsToken, expression, semicolonToken);
        }

        public static BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken opToken, ExpressionSyntax right)
        {
            return new BinaryExpressionSyntax(kind, left, opToken, right);
        }

        public static LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
        {
            if (kind != SyntaxKind.StringLiteralExpression && kind != SyntaxKind.NumericLiteralExpression)
            {
                throw new ArgumentException("kind");
            }

            // TODO ValidateLiteralExpressionParts

            return new LiteralExpressionSyntax(kind, token);
        }

        public static IdentifierNameSyntax IdentifierName(SyntaxToken identifier)
        {
            return new IdentifierNameSyntax(SyntaxKind.IdentifierName, identifier);
        }
    }
}
