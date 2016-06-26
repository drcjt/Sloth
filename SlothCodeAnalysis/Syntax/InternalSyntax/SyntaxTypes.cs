using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal abstract class StatementSyntax : SyntaxNode
    {
        internal StatementSyntax(SyntaxKind kind) : base(kind)
        {
        }
    }

    internal abstract class ExpressionSyntax : SyntaxNode
    {
        internal ExpressionSyntax(SyntaxKind kind) : base(kind)
        {
        }
    }

    internal class LiteralExpressionSyntax : ExpressionSyntax
    {
        internal readonly SyntaxToken _token;

        internal LiteralExpressionSyntax(SyntaxKind kind, SyntaxToken token) : base(kind)
        {
            _token = token;
        }
    }

    internal class IdentifierNameSyntax : ExpressionSyntax
    {
        internal readonly SyntaxToken _identifier;

        internal IdentifierNameSyntax(SyntaxKind kind, SyntaxToken identifier) : base(kind)
        {
            _identifier = identifier;
        }
    }

    internal class BinaryExpressionSyntax : ExpressionSyntax
    {
        internal readonly ExpressionSyntax _left;
        internal readonly SyntaxToken _operatorToken;
        internal readonly ExpressionSyntax _right;

        internal BinaryExpressionSyntax(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right) : base(kind)
        {
            _left = left;
            _operatorToken = operatorToken;
            _right = right;
        }
    }

    internal class VariableDeclarationSyntax : StatementSyntax
    {
        internal readonly SyntaxToken _varKeyword;
        internal readonly SyntaxToken _identifier;
        internal readonly SyntaxToken _equalsToken;
        internal readonly ExpressionSyntax _expression;
        internal readonly SyntaxToken _semicolonToken;

        internal VariableDeclarationSyntax(SyntaxKind kind, SyntaxToken varKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression, SyntaxToken semicolonToken) : base(kind)
        {
            _varKeyword = varKeyword;
            _identifier = identifier;
            _equalsToken = equalsToken;
            _expression = expression;
            _semicolonToken = semicolonToken;
        }
    }

    internal class AssignmentSyntax : StatementSyntax
    {
        internal readonly SyntaxToken _identifier;
        internal readonly SyntaxToken _equalsToken;
        internal readonly ExpressionSyntax _expression;
        internal readonly SyntaxToken _semicolonToken;

        internal AssignmentSyntax(SyntaxKind kind, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression, SyntaxToken semicolonToken) : base(kind)
        {
            _identifier = identifier;
            _equalsToken = equalsToken;
            _expression = expression;
            _semicolonToken = semicolonToken;
        }
    }

    internal class ForStatementSyntax : StatementSyntax
    {
        internal readonly SyntaxToken _forKeyword;
        internal readonly SyntaxToken _identifier;
        internal readonly SyntaxToken _equalsToken;
        internal readonly ExpressionSyntax _lower;
        internal readonly SyntaxToken _toKeyword;
        internal readonly ExpressionSyntax _upper;
        internal readonly SyntaxToken _doKeyword;
        internal readonly SyntaxList<StatementSyntax> _statements;
        internal readonly SyntaxToken _endKeyword;
        internal readonly SyntaxToken _semicolonToken;

        internal ForStatementSyntax(SyntaxKind kind, SyntaxToken forKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax lower, SyntaxToken toKeyword, ExpressionSyntax upper, SyntaxToken doKeyword, SyntaxList<StatementSyntax> statements, SyntaxToken endKeyword, SyntaxToken semicolonToken) : base(kind)
        {
            _forKeyword = forKeyword;
            _identifier = identifier;
            _equalsToken = equalsToken;
            _lower = lower;
            _toKeyword = toKeyword;
            _upper = upper;
            _doKeyword = doKeyword;
            _statements = statements;
            _endKeyword = endKeyword;
            _semicolonToken = semicolonToken;
        }
    }

    internal class ReadIntSyntax : StatementSyntax
    {
        internal SyntaxToken _readIntKeyword;
        internal SyntaxToken _identifier;
        internal SyntaxToken _semicolonToken;

        internal ReadIntSyntax(SyntaxKind kind, SyntaxToken readIntKeyword, SyntaxToken identifier, SyntaxToken semicolonToken) : base(kind)
        {
            _readIntKeyword = readIntKeyword;
            _identifier = identifier;
            _semicolonToken = semicolonToken;
        }
    }

    /*
    internal class ReadStringSyntax : StatementSyntax
    {
        internal SyntaxToken _readStringKeyword;
        internal SyntaxToken _identifier;

        internal ReadStringSyntax(SyntaxKind kind, SyntaxToken readStringKeyword, SyntaxToken identifier) : base(kind)
        {
            _readStringKeyword = readStringKeyword;
            _identifier = identifier;
        }
    }
    */

    internal class PrintStatementSyntax : StatementSyntax
    {
        internal readonly SyntaxToken _printKeyword;
        internal readonly ExpressionSyntax _expression;
        internal readonly SyntaxToken _semicolonToken;

        internal PrintStatementSyntax(SyntaxKind kind, SyntaxToken printKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken) : base(kind)
        {
            _printKeyword = printKeyword;
            _expression = expression;
            _semicolonToken = semicolonToken;
        }
    }

    internal class EmptyStatementSyntax : StatementSyntax
    {
        internal readonly SyntaxToken _semicolonToken;

        internal EmptyStatementSyntax(SyntaxKind kind, SyntaxToken semicolonToken) : base(kind)
        {
            _semicolonToken = semicolonToken;
        }
    }

    internal class CompilationUnitSyntax : StatementSyntax
    {
        internal readonly SyntaxList<StatementSyntax> _statements;
        internal readonly SyntaxToken _endOfFileToken;

        internal CompilationUnitSyntax(SyntaxKind kind, SyntaxList<StatementSyntax> statements, SyntaxToken endOfFileToken) : base(kind)
        {
            _statements = statements;
            _endOfFileToken = endOfFileToken;
        }
    }
}
