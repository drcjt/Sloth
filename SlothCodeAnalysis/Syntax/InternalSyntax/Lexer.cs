using SlothCodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class Lexer : IDisposable
    {
        private readonly SlidingTextWindow TextWindow;

        public const char InvalidCharacter = char.MaxValue;

        public Lexer(SourceText text)
        {
            TextWindow = new SlidingTextWindow(text);
            _keywordKindMap = new Dictionary<string, SyntaxKind>()
            {
                { "var", SyntaxKind.VarKeyword },
                { "for", SyntaxKind.ForKeyword },
                { "to", SyntaxKind.ToKeyword },
                { "do", SyntaxKind.DoKeyword },
                { "end", SyntaxKind.EndKeyword },
                { "read_int", SyntaxKind.ReadIntKeyword },
                { "print", SyntaxKind.PrintKeyword }
            };
        }

        public SyntaxToken Lex()
        {
            return LexSyntaxToken();
        }

        private SyntaxToken LexSyntaxToken()
        {
            // Lex leading syntax trivia
            var leadingTrivia = TextWindow.Position == 0 ? LexSyntaxTrivia() : null;

            var tokenInfo = ScanSyntaxToken();

            // Lex trailing syntax trivia
            var trailingTrivia = LexSyntaxTrivia();

            return Create(tokenInfo.Kind, tokenInfo.Text, tokenInfo.Value, leadingTrivia, trailingTrivia);
        }

        private string LexSyntaxTrivia()
        {
            TextWindow.Start();
         
            var ch = TextWindow.PeekChar();
            while (char.IsWhiteSpace(ch))
            {
                TextWindow.AdvanceChar();
                ch = TextWindow.PeekChar();
            }

            return TextWindow.GetText();
        }

        internal struct TokenInfo
        {
            internal SyntaxKind Kind;
            internal string Text;
            internal object Value;
        }

        private readonly Dictionary<string, SyntaxKind> _keywordKindMap;

        private TokenInfo ScanSyntaxToken()
        {
            var startOffset = TextWindow.Position;

            SyntaxKind kind = SyntaxKind.None;
            object value = null;

            // Lex token
            var ch = TextWindow.PeekChar();
            if (char.IsLetter(ch))
            {
                // Lex Identifer
                kind = SyntaxKind.IdentifierToken;
                value = ScanIdentifier(ch);

                // Determine if identifier is a keyword
                SyntaxKind keywordKind;
                _keywordKindMap.TryGetValue((string)value, out keywordKind);
                if (keywordKind != SyntaxKind.None)
                {
                    kind = keywordKind;
                }
            }
            else if (ch == '"')
            {
                kind = SyntaxKind.StringLiteralToken;
                value = ScanStringLiteral();
            }
            else if (char.IsDigit(ch))
            {
                kind = SyntaxKind.NumericLiteralToken;
                value = ScanNumericLiteral(ch);
            }
            else if (ch == ';')
            {
                TextWindow.AdvanceChar();
                kind = SyntaxKind.SemicolonToken;
            }
            else if (ch == InvalidCharacter)
            {
                kind = SyntaxKind.EndOfFileToken;
            }
            else
            {
                switch (ch)
                {
                    case '+':
                        TextWindow.AdvanceChar();
                        kind = SyntaxKind.PlusToken;
                        break;
                    case '-':
                        TextWindow.AdvanceChar();
                        kind = SyntaxKind.MinusToken;
                        break;
                    case '*':
                        TextWindow.AdvanceChar();
                        kind = SyntaxKind.AsteriskToken;
                        break;
                    case '/':
                        TextWindow.AdvanceChar();
                        kind = SyntaxKind.SlashToken;
                        break;
                    case '=':
                        TextWindow.AdvanceChar();
                        kind = SyntaxKind.EqualsToken;
                        break;
                    default:
                        TextWindow.AdvanceChar();
                        break;
                }
            }

            var text = TextWindow.GetText(startOffset, TextWindow.Position - startOffset);

            // Return syntax token
            var tokenInfo = default(TokenInfo);
            tokenInfo.Kind = kind;
            tokenInfo.Text = text;
            tokenInfo.Value = value;

            return tokenInfo;
        }

        private string ScanIdentifier(char ch)
        {
            TextWindow.Start();

            while (char.IsLetter(ch) || ch == '_')
            {
                TextWindow.AdvanceChar();
                ch = TextWindow.PeekChar();
            }

            return TextWindow.GetText();
        }

        private string ScanStringLiteral()
        {
            // Skip the '"'
            TextWindow.AdvanceChar();

            var ch = TextWindow.PeekChar();

            if (ch == InvalidCharacter)
            {
                return string.Empty;
            }

            int start = TextWindow.Position;
            int width = 0;
            while (ch != '"' && ch != InvalidCharacter)
            {
                TextWindow.AdvanceChar();
                ch = TextWindow.PeekChar();
                width++;
            }

            // Skip the '"'
            if (ch == '"')
            {
                TextWindow.AdvanceChar();
            }

            return TextWindow.GetText(start, width);
        }

        public int ScanNumericLiteral(char ch)
        {
            TextWindow.Start();
            while (char.IsDigit(ch))
            {
                TextWindow.AdvanceChar();
                ch = TextWindow.PeekChar();
            }

            return int.Parse(TextWindow.GetText());
        }

        private SyntaxToken Create(SyntaxKind kind, string text, object value, string leadingTrivia, string trailingTrivia)
        {
            SyntaxToken token;
            switch (kind)
            {
                case SyntaxKind.IdentifierToken:
                    token = SyntaxFactory.Identifier(leadingTrivia, text, trailingTrivia);
                    break;

                case SyntaxKind.NumericLiteralToken:
                    token = SyntaxFactory.Literal(leadingTrivia, text, (int)value, trailingTrivia);
                    break;

                case SyntaxKind.StringLiteralToken:
                    token = SyntaxFactory.Literal(leadingTrivia, text, trailingTrivia);
                    break;

                case SyntaxKind.None:
                    token = SyntaxFactory.BadToken(leadingTrivia, text, trailingTrivia);
                    break;

                default:
                    token = SyntaxFactory.Token(leadingTrivia, kind, trailingTrivia);
                    break;
            }

            return token;
        }

        void IDisposable.Dispose()
        {
            // Dispose any resources here
        }
    }
}
