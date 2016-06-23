namespace SlothCodeAnalysis.Syntax
{
    public static class SyntaxKindFacts
    {
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
