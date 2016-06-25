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

    internal class VariableSyntax : ExpressionSyntax
    {
        internal readonly SyntaxToken _variableIdentifier;

        internal VariableSyntax(SyntaxKind kind, SyntaxToken variableIdentifier) : base(kind)
        {
            _variableIdentifier = variableIdentifier;
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

        internal VariableDeclarationSyntax(SyntaxKind kind, SyntaxToken varKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression) : base(kind)
        {
            _varKeyword = varKeyword;
            _identifier = identifier;
            _equalsToken = equalsToken;
            _expression = expression;                 
        }
    }

    internal class AssignmentSyntax : StatementSyntax
    {
        internal readonly SyntaxToken _identifier;
        internal readonly SyntaxToken _equalsToken;
        internal readonly ExpressionSyntax _expression;

        internal AssignmentSyntax(SyntaxKind kind, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression) : base(kind)
        {
            _identifier = identifier;
            _equalsToken = equalsToken;
            _expression = expression;
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
        internal readonly StatementSyntax _statement;
        internal readonly SyntaxToken _endKeyword;

        internal ForStatementSyntax(SyntaxKind kind, SyntaxToken forKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax lower, SyntaxToken toKeyword, ExpressionSyntax upper, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken endKeyword) : base(kind)
        {
            _forKeyword = forKeyword;
            _identifier = identifier;
            _equalsToken = equalsToken;
            _lower = lower;
            _toKeyword = toKeyword;
            _upper = upper;
            _doKeyword = doKeyword;
            _statement = statement;
            _endKeyword = endKeyword;
        }
    }

    internal class ReadIntSyntax : StatementSyntax
    {
        internal SyntaxToken _readIntKeyword;
        internal SyntaxToken _identifier;

        internal ReadIntSyntax(SyntaxKind kind, SyntaxToken readIntKeyword, SyntaxToken identifier) : base(kind)
        {
            _readIntKeyword = readIntKeyword;
            _identifier = identifier;
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

    internal class SequenceSyntax : StatementSyntax
    {
        internal StatementSyntax _first;
        internal StatementSyntax _second;

        internal SequenceSyntax(SyntaxKind kind, StatementSyntax first, StatementSyntax second) : base(kind)
        {
            _first = first;
            _second = second;
        }
    }

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
