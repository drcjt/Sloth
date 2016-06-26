namespace SlothCodeAnalysis.Syntax
{
    public static class SyntaxFacts
    {
        public static bool IsBinaryExpression(SyntaxKind token)
        {
            return GetBinaryExpression(token) != SyntaxKind.None;
        }

        public static SyntaxKind GetBinaryExpression(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.PlusToken:
                    return SyntaxKind.AddExpression;
                case SyntaxKind.MinusToken:
                    return SyntaxKind.SubtractExpression;
                case SyntaxKind.AsteriskToken:
                    return SyntaxKind.MultiplyExpression;
                case SyntaxKind.SlashToken:
                    return SyntaxKind.DivideExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        public static SyntaxKind GetLiteralExpression(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.StringLiteralToken:
                    return SyntaxKind.StringLiteralExpression;
                case SyntaxKind.NumericLiteralToken:
                    return SyntaxKind.NumericLiteralExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        public static string GetText(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.AsteriskToken:
                    return "*";
                case SyntaxKind.EqualsToken:
                    return "=";
                case SyntaxKind.MinusToken:
                    return "-";
                case SyntaxKind.PlusToken:
                    return "+";
                case SyntaxKind.SemicolonToken:
                    return ";";
                case SyntaxKind.SlashToken:
                    return "/";

                case SyntaxKind.DoKeyword:
                    return "do";
                case SyntaxKind.EndKeyword:
                    return "end";
                case SyntaxKind.ForKeyword:
                    return "for";
                case SyntaxKind.PrintKeyword:
                    return "print";
                case SyntaxKind.ReadIntKeyword:
                    return "read_int";
                case SyntaxKind.ReadStringKeyword:
                    return "read_string";
                case SyntaxKind.ToKeyword:
                    return "to";
                case SyntaxKind.VarKeyword:
                    return "var";

                default:
                    return string.Empty;
            }
        }
    }
}
