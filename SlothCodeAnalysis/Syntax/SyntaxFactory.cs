using System.Collections.Generic;

namespace SlothCodeAnalysis.Syntax
{
    public static class SyntaxFactory
    {
        public static IEnumerable<SyntaxToken> ParseTokens(string text)
        {
            using (var lexer = new InternalSyntax.Lexer(text))
            {
                var endOfFile = false;
                while (!endOfFile)
                {
                    var token = lexer.Lex();
                    yield return new SyntaxToken(token);

                    endOfFile = token.Kind == SyntaxKind.EndOfFileToken;
                }
            }
        }

        // Static helpers to create Syntax instances
        public static SyntaxToken Token(SyntaxKind kind)
        {
            return new SyntaxToken(InternalSyntax.SyntaxFactory.Token(kind));
        }

        public static SyntaxToken Identifier(string text)
        {
            return new SyntaxToken(InternalSyntax.SyntaxFactory.Identifier(text));
        }

        public static SyntaxToken Literal(int value)
        {
            return new SyntaxToken(InternalSyntax.SyntaxFactory.Literal(value.ToString(), value));
        }

        public static SyntaxToken Literal(string value)
        {
            return new SyntaxToken(InternalSyntax.SyntaxFactory.Literal(value));
        }

        private static InternalSyntax.Lexer MakeLexer(string text)
        {
            return new InternalSyntax.Lexer(text);
        }

        private static InternalSyntax.LanguageParser MakeParser(InternalSyntax.Lexer lexer)
        {
            return new InternalSyntax.LanguageParser(lexer);
        }

        public static CompilationUnitSyntax ParseCompilationUnit(string text)
        {
            using (var lexer = MakeLexer(text))
            {
                using (var parser = MakeParser(lexer))
                {
                    var node = parser.ParseCompilationUnit();
                    return (CompilationUnitSyntax)node.CreateRedNode();
                }
            }
        }
    }
}
