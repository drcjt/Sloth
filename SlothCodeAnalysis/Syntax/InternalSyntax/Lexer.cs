using System;
using System.Text;

namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class Lexer : IDisposable
    {
        private readonly string _sourceText;
        private int _offset;
        public const char InvalidCharacter = char.MaxValue;

        public Lexer(string source)
        {
            _sourceText = source;
            _offset = 0;
        }

        private char PeekChar()
        {
            if (_offset >= _sourceText.Length)
            {
                return InvalidCharacter;
            }

            return _sourceText[_offset];
        }

        private void AdvanceChar()
        {
            _offset++;
        }

        private char NextChar()
        {
            var c = PeekChar();
            if (c != InvalidCharacter)
            {
                _offset++;
            }
            return c;
        }

        public SyntaxToken Lex()
        {
            return LexSyntaxToken();
        }

        private SyntaxToken LexSyntaxToken()
        {
            // Lex leading syntax trivia
            var leadingTrivia = _offset == 0 ? LexSyntaxTrivia() : null;

            var tokenInfo = ScanSyntaxToken();

            // Lex trailing syntax trivia
            var trailingTrivia = LexSyntaxTrivia();

            return Create(tokenInfo.Kind, tokenInfo.Text, tokenInfo.Value, leadingTrivia, trailingTrivia);
        }

        private string LexSyntaxTrivia()
        {
            var startOffset = _offset;

            var ch = PeekChar();
            while (char.IsWhiteSpace(ch))
            {
                AdvanceChar();
                ch = PeekChar();
            }

            if (_offset > startOffset)
            {
                return _sourceText.Substring(startOffset, _offset - startOffset);
            }

            return string.Empty;
        }

        internal struct TokenInfo
        {
            internal SyntaxKind Kind;
            internal string Text;
            internal object Value;
        }

        private TokenInfo ScanSyntaxToken()
        {
            var startOffset = _offset;

            SyntaxKind kind = SyntaxKind.None;
            object value = null;

            // Lex token
            var ch = PeekChar();
            if (char.IsLetter(ch))
            {
                // Lex Identifer
                kind = SyntaxKind.IdentifierToken;
                value = ScanIdentifier(ch);
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
                AdvanceChar();
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
                        AdvanceChar();
                        kind = SyntaxKind.PlusToken;
                        break;
                    case '-':
                        AdvanceChar();
                        kind = SyntaxKind.MinusToken;
                        break;
                    case '*':
                        AdvanceChar();
                        kind = SyntaxKind.AsteriskToken;
                        break;
                    case '/':
                        AdvanceChar();
                        kind = SyntaxKind.SlashToken;
                        break;
                    case '=':
                        AdvanceChar();
                        kind = SyntaxKind.EqualsToken;
                        break;
                    default:
                        throw new Exception($"Encountered unrecognized character '{ch}'");
                }
            }

            var text = _sourceText.Substring(startOffset, _offset - startOffset);

            // Return syntax token
            var tokenInfo = default(TokenInfo);
            tokenInfo.Kind = kind;
            tokenInfo.Text = text;
            tokenInfo.Value = value;

            return tokenInfo;
        }

        private string ScanIdentifier(char ch)
        {
            var identifier = new StringBuilder();

            while (char.IsLetter(ch) || ch == '_')
            {
                identifier.Append(ch);
                AdvanceChar();

                ch = PeekChar();
                if (ch == InvalidCharacter)
                {
                    break;
                }
            }

            return identifier.ToString();
        }

        private string ScanStringLiteral()
        {
            // Skip the '"'
            AdvanceChar();

            var ch = PeekChar();

            if (ch == InvalidCharacter)
            {
                throw new Exception("Unterminated string literal");
            }

            var stringLiteral = new StringBuilder();
            while (ch != '"')
            {
                stringLiteral.Append(ch);
                AdvanceChar();

                ch = PeekChar();
                if (ch == InvalidCharacter)
                {
                    throw new Exception("Unterminated string literal");
                }
            }

            // Skip the '"'
            AdvanceChar();

            return stringLiteral.ToString();
        }

        public int ScanNumericLiteral(char ch)
        {
            var numericLiteral = new StringBuilder();
            while (char.IsDigit(ch))
            {
                numericLiteral.Append(ch);

                AdvanceChar();

                ch = PeekChar();
                if (ch == InvalidCharacter)
                {
                    break;
                }
            }

            return int.Parse(numericLiteral.ToString());
        }

        private SyntaxToken Create(SyntaxKind kind, string text, object value, string leadingTrivia, string trailingTrivia)
        {
            SyntaxToken token = null;
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
