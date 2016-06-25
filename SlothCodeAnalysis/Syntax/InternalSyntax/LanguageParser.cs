using SlothCodeAnalysis.InternalUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class LanguageParser : IDisposable
    {
        private readonly Lexer _lexer;

        internal LanguageParser(Lexer lexer)
        {
            _lexer = lexer;
            _lexedTokens = new SyntaxToken[32];
        }

        private SyntaxToken[] _lexedTokens;
        private int _tokenCount;
        private int _tokenOffset;
        private SyntaxToken _currentToken;

        private SyntaxToken CurrentToken
        {
            get
            {
                return _currentToken ?? (_currentToken = FetchCurrentToken());
            }
        }

        private SyntaxToken FetchCurrentToken()
        {
            if (_tokenOffset >= _tokenCount)
            {
                AddNewToken();
            }

            return _lexedTokens[_tokenOffset];
        }

        private void AddNewToken()
        {
            AddLexedToken(_lexer.Lex());
        }

        private void AddLexedToken(SyntaxToken token)
        {
            _lexedTokens[_tokenCount] = token;
            _tokenCount++;
        }        

        private void MoveToNextToken()
        {
            _currentToken = default(SyntaxToken);
            _tokenOffset++;
        }

        protected SyntaxToken EatToken(SyntaxKind kind)
        {
            var ct = CurrentToken;
            if (ct.Kind == kind)
            {
                MoveToNextToken();
                return ct;
            }

            return CreateMissingToken(kind, CurrentToken.Kind);
        }

        private SyntaxToken CreateMissingToken(SyntaxKind expected, SyntaxKind actual)
        {
            var token = SyntaxFactory.MissingToken(expected);

            // TODO : Add diagnostics to token containing the actual kind

            return token;
        }

        internal CompilationUnitSyntax ParseCompilationUnit()
        {
            return ParseWithStackGuard(ParseCompilationUnitCore, () => SyntaxFactory.CompilationUnit(new SyntaxList<StatementSyntax>(), SyntaxFactory.Token(SyntaxKind.EndOfFileToken)));
        }

        internal CompilationUnitSyntax ParseCompilationUnitCore()
        {
            var statements = ParseStatements();
            var eof = EatToken(SyntaxKind.EndOfFileToken);
            return SyntaxFactory.CompilationUnit(statements, eof);
        }

        internal SyntaxList<StatementSyntax> ParseStatements()
        {
            var statements = new SyntaxListBuilder<StatementSyntax>();

            while (IsPossibleStatement())
            {
                var statement = ParseStatementCore();

                // TODO - What if the statement is null??

                statements.Add(statement);
            }

            return statements.ToListNode();
        }

        private bool IsPossibleStatement()
        {
            var tk = CurrentToken.Kind;
            switch (tk)
            {
                case SyntaxKind.PrintKeyword:
                case SyntaxKind.VarKeyword:
                case SyntaxKind.ReadIntKeyword:
                case SyntaxKind.ForKeyword:
                case SyntaxKind.IdentifierToken:
                    return true;

                default:
                    return false;
            }
        }

        internal StatementSyntax ParseStatementCore()
        {
            switch (CurrentToken.Kind)
            {
                case SyntaxKind.PrintKeyword:
                    return ParsePrintStatement();
                default:
                    return null;
            }
        }

        private PrintStatementSyntax ParsePrintStatement()
        {
            var print = EatToken(SyntaxKind.PrintKeyword);
            var expression = ParseExpressionCore();
            var semicolon = EatToken(SyntaxKind.SemicolonToken);

            return SyntaxFactory.PrintStatement(print, expression, semicolon);
        }

        private ExpressionSyntax ParseExpressionCore()
        {
            return null;
        }

        internal TNode ParseWithStackGuard<TNode>(Func<TNode> parserFunc, Func<TNode> createEmptyNodeFunc) where TNode : SyntaxNode
        {
            try
            {
                return parserFunc();
            }
            catch (Exception ex) when (StackGuard.IsInsufficientExecutionStackException(ex))
            {
                return createEmptyNodeFunc();
            }
        }

        void IDisposable.Dispose()
        {
        }
    }
}
