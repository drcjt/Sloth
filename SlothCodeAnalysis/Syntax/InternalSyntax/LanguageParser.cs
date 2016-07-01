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
            _lexedTokens = new SyntaxToken[1000];
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

        private static SyntaxToken CreateMissingIdentifierToken()
        {
            return SyntaxFactory.MissingToken(SyntaxKind.IdentifierToken);
        }

        private IdentifierNameSyntax CreateMissingIdentifierName()
        {
            return SyntaxFactory.IdentifierName(CreateMissingIdentifierToken());
        }

        internal CompilationUnitSyntax ParseCompilationUnit()
        {
            return ParseWithStackGuard(ParseCompilationUnitCore, () => SyntaxFactory.CompilationUnit(new SyntaxList<StatementSyntax>(new SyntaxList()), SyntaxFactory.Token(SyntaxKind.EndOfFileToken)));
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
                case SyntaxKind.VarKeyword:
                    return ParseVariableDeclaration();
                case SyntaxKind.ReadIntKeyword:
                    return ParseReadIntStatement();
                case SyntaxKind.ForKeyword:
                    return ParseForStatement();
                case SyntaxKind.IdentifierToken:
                    return ParseAssignment();
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

        private VariableDeclarationSyntax ParseVariableDeclaration()
        {
            var varKeyword = EatToken(SyntaxKind.VarKeyword);
            var identifier = ParseIdentifierToken();
            var equalsToken = EatToken(SyntaxKind.EqualsToken);
            var expression = ParseExpressionCore();
            var semicolon = EatToken(SyntaxKind.SemicolonToken);

            return SyntaxFactory.VariableDeclaration(varKeyword, identifier, equalsToken, expression, semicolon);
        }

        private ReadIntSyntax ParseReadIntStatement()
        {
            var readIntKeyword = EatToken(SyntaxKind.ReadIntKeyword);
            var identifier = ParseIdentifierToken();
            var semicolon = EatToken(SyntaxKind.SemicolonToken);

            return SyntaxFactory.ReadInt(readIntKeyword, identifier, semicolon);
        }

        private ForStatementSyntax ParseForStatement()
        {
            var forKeyword = EatToken(SyntaxKind.ForKeyword);
            var identifier = ParseIdentifierToken();
            var equalsToken = EatToken(SyntaxKind.EqualsToken);
            var lower = ParseExpressionCore();
            var toKeyword = EatToken(SyntaxKind.ToKeyword);
            var upper = ParseExpressionCore();
            var doKeyword = EatToken(SyntaxKind.DoKeyword);
            var body = ParseStatements();
            var endKeyword = EatToken(SyntaxKind.EndKeyword);
            var semicolonToken = EatToken(SyntaxKind.SemicolonToken);

            return SyntaxFactory.For(forKeyword, identifier, equalsToken, lower, toKeyword, upper, doKeyword, body, endKeyword, semicolonToken);
        }

        private AssignmentSyntax ParseAssignment()
        {
            var identifier = ParseIdentifierToken();
            var equalsToken = EatToken(SyntaxKind.EqualsToken);
            var expression = ParseExpressionCore();
            var semicolonToken = EatToken(SyntaxKind.SemicolonToken);

            return SyntaxFactory.Assignment(identifier, equalsToken, expression, semicolonToken);
        }

        private SyntaxToken ParseIdentifierToken()
        {
            var ctk = CurrentToken.Kind;
            if (ctk == SyntaxKind.IdentifierToken)
            {
                SyntaxToken identifierToken = EatToken(SyntaxKind.IdentifierToken);
                return identifierToken;
            }
            else
            {
                var name = CreateMissingIdentifierToken();
                // TODO - add error for expected identifier
                return name;
            }
        }

        private ExpressionSyntax ParseExpressionCore()
        {
            return ParseSubExpression(Precedence.Expression);
        }

        enum Precedence
        {
            Expression = 0,
            Additive,
            Multiplicative
        }

        private ExpressionSyntax ParseSubExpression(Precedence precedence)
        {
            ExpressionSyntax leftOperand = null;
            Precedence newPrecedence = 0;
            SyntaxKind opKind = SyntaxKind.None;

            var tk = CurrentToken.Kind;
            leftOperand = ParseTerm(precedence);

            while (true)
            {
                tk = CurrentToken.Kind;

                if (IsExpectedBinaryOperator(tk))
                {
                    opKind = SyntaxFacts.GetBinaryExpression(tk);
                }
                else
                {
                    break;
                }

                newPrecedence = GetPrecedence(opKind);

                if (newPrecedence <= precedence)
                {
                    break;
                }

                var opToken = EatToken(tk);

                leftOperand = SyntaxFactory.BinaryExpression(opKind, leftOperand, opToken, ParseSubExpression(newPrecedence));
            }

            return leftOperand;
        }

        private static Precedence GetPrecedence(SyntaxKind op)
        {
            switch (op)
            {
                case SyntaxKind.AddExpression:
                case SyntaxKind.SubtractExpression:
                    return Precedence.Additive;
                case SyntaxKind.MultiplyExpression:
                case SyntaxKind.DivideExpression:
                    return Precedence.Multiplicative;
                default:
                    return Precedence.Expression;
            }
        }

        private static bool IsExpectedBinaryOperator(SyntaxKind kind)
        {
            return SyntaxFacts.IsBinaryExpression(kind);
        }

        private ExpressionSyntax ParseTerm(Precedence precedence)
        {
            ExpressionSyntax expr = null;

            var tk = CurrentToken.Kind;
            switch (tk)
            {
                case SyntaxKind.IdentifierToken:
                    expr = SyntaxFactory.IdentifierName(ParseIdentifierToken());
                    break;
                case SyntaxKind.NumericLiteralToken:
                case SyntaxKind.StringLiteralToken:
                    expr = SyntaxFactory.LiteralExpression(SyntaxFacts.GetLiteralExpression(tk), EatToken(tk));
                    break;
                default:
                    expr = CreateMissingIdentifierName();
                    break;
            }

            return expr;
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
