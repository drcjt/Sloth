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
            SlotCount = 1;
            _token = token;
            AdjustWidth(_token);
        }

        public SyntaxToken Token { get { return _token; } }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.LiteralExpressionSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _token;
                default: return null;
            }
        }
    }

    internal class IdentifierNameSyntax : ExpressionSyntax
    {
        internal readonly SyntaxToken _identifier;

        internal IdentifierNameSyntax(SyntaxKind kind, SyntaxToken identifier) : base(kind)
        {
            SlotCount = 1;
            _identifier = identifier;
            AdjustWidth(_identifier);
        }

        public SyntaxToken Identifier { get { return _identifier; } }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.IdentifierNameSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _identifier;
                default: return null;
            }
        }

    }

    internal class BinaryExpressionSyntax : ExpressionSyntax
    {
        internal readonly ExpressionSyntax _left;
        internal readonly SyntaxToken _operatorToken;
        internal readonly ExpressionSyntax _right;

        internal BinaryExpressionSyntax(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right) : base(kind)
        {
            SlotCount = 3;
            _left = left;
            AdjustWidth(_left);
            _operatorToken = operatorToken;
            AdjustWidth(_operatorToken);
            _right = right;
            AdjustWidth(_right);
        }

        public ExpressionSyntax Left { get { return _left; } }
        public SyntaxToken OperatorToken { get { return _operatorToken; } }
        public ExpressionSyntax Right { get { return _right; } }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.BinaryExpressionSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _left;
                case 1: return _operatorToken;
                case 2: return _right;
                default: return null;
            }
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
            SlotCount = 5;
            _varKeyword = varKeyword;
            AdjustWidth(_varKeyword);
            _identifier = identifier;
            AdjustWidth(_identifier);
            _equalsToken = equalsToken;
            AdjustWidth(_equalsToken);
            _expression = expression;
            AdjustWidth(_expression);
            _semicolonToken = semicolonToken;
            AdjustWidth(_semicolonToken);
        }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.VariableDeclarationSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _varKeyword;
                case 1: return _identifier;
                case 2: return _equalsToken;
                case 3: return _expression;
                case 4: return _semicolonToken;
                default: return null;
            }
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
            SlotCount = 4;
            _identifier = identifier;
            AdjustWidth(_identifier);
            _equalsToken = equalsToken;
            AdjustWidth(_equalsToken);
            _expression = expression;
            AdjustWidth(_expression);
            _semicolonToken = semicolonToken;
            AdjustWidth(_semicolonToken);
        }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.AssignmentSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _identifier;
                case 1: return _equalsToken;
                case 2: return _expression;
                case 3: return _semicolonToken;
                default: return null;
            }
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
        internal readonly SyntaxList _statements;
        internal readonly SyntaxToken _endKeyword;
        internal readonly SyntaxToken _semicolonToken;

        internal ForStatementSyntax(SyntaxKind kind, SyntaxToken forKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax lower, SyntaxToken toKeyword, ExpressionSyntax upper, SyntaxToken doKeyword, SyntaxList statements, SyntaxToken endKeyword, SyntaxToken semicolonToken) : base(kind)
        {
            SlotCount = 10;
            _forKeyword = forKeyword;
            AdjustWidth(_forKeyword);
            _identifier = identifier;
            AdjustWidth(_identifier);
            _equalsToken = equalsToken;
            AdjustWidth(_equalsToken);
            _lower = lower;
            AdjustWidth(_lower);
            _toKeyword = toKeyword;
            AdjustWidth(_toKeyword);
            _upper = upper;
            AdjustWidth(_upper);
            _doKeyword = doKeyword;
            AdjustWidth(_doKeyword);
            _statements = statements;
            AdjustWidth(_statements);
            _endKeyword = endKeyword;
            AdjustWidth(_endKeyword);
            _semicolonToken = semicolonToken;
            AdjustWidth(_semicolonToken);
        }

        public SyntaxList<StatementSyntax> Statements { get { return new SyntaxList<StatementSyntax>(_statements); } }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.ForStatementSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _forKeyword;
                case 1: return _identifier;
                case 2: return _equalsToken;
                case 3: return _lower;
                case 4: return _toKeyword;
                case 5: return _upper;
                case 6: return _doKeyword;
                case 7: return _statements;
                case 8: return _equalsToken;
                case 9: return _semicolonToken;
                default: return null;
            }
        }
    }

    internal class ReadIntSyntax : StatementSyntax
    {
        internal SyntaxToken _readIntKeyword;
        internal SyntaxToken _identifier;
        internal SyntaxToken _semicolonToken;

        internal ReadIntSyntax(SyntaxKind kind, SyntaxToken readIntKeyword, SyntaxToken identifier, SyntaxToken semicolonToken) : base(kind)
        {
            SlotCount = 3;
            _readIntKeyword = readIntKeyword;
            AdjustWidth(_readIntKeyword);
            _identifier = identifier;
            AdjustWidth(_identifier);
            _semicolonToken = semicolonToken;
            AdjustWidth(_semicolonToken);
        }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.ReadIntSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _readIntKeyword;
                case 1: return _identifier;
                case 2: return _semicolonToken;
                default: return null;
            }
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
            SlotCount = 3;
            _printKeyword = printKeyword;
            AdjustWidth(_printKeyword);
            _expression = expression;
            AdjustWidth(_expression);
            _semicolonToken = semicolonToken;
            AdjustWidth(_semicolonToken);
        }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.PrintStatementSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _printKeyword;
                case 1: return _expression;
                case 2: return _semicolonToken;
                default: return null;
            }
        }
    }

    internal class EmptyStatementSyntax : StatementSyntax
    {
        internal readonly SyntaxToken _semicolonToken;

        public SyntaxToken SemicolonToken { get { return _semicolonToken; } }

        internal EmptyStatementSyntax(SyntaxKind kind, SyntaxToken semicolonToken) : base(kind)
        {
            SlotCount = 1;
            _semicolonToken = semicolonToken;
            AdjustWidth(_semicolonToken);
        }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.EmptyStatementSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _semicolonToken;
                default: return null;
            }
        }

    }

    internal class CompilationUnitSyntax : SyntaxNode
    {
        internal readonly SyntaxList _statements;
        internal readonly SyntaxToken _endOfFileToken;

        internal CompilationUnitSyntax(SyntaxKind kind, SyntaxList statements, SyntaxToken endOfFileToken) : base(kind)
        {
            SlotCount = 2;
            _statements = statements;
            AdjustWidth(_statements);
            _endOfFileToken = endOfFileToken;
            AdjustWidth(_endOfFileToken);
        }

        public SyntaxList<StatementSyntax> Statements { get { return new SyntaxList<StatementSyntax>(_statements); } }

        internal override RedNode CreateRedNode(RedNode parent, int position)
        {
            return new SlothCodeAnalysis.Syntax.CompilationUnitSyntax(this, parent, position);
        }

        internal override GreenNode GetSlot(int index)
        {
            switch (index)
            {
                case 0: return _statements;
                case 1: return _endOfFileToken;
                default: return null;
            }
        }
    }
}
