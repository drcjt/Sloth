namespace SlothCodeAnalysis.Syntax
{
    public abstract class StatementSyntax : SyntaxNode
    {
        internal StatementSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }
    }

    public abstract class ExpressionSyntax : SyntaxNode
    {
        internal ExpressionSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }
    }

    public class LiteralExpressionSyntax : ExpressionSyntax
    {
        internal LiteralExpressionSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxToken Token
        {
            get { return new SyntaxToken(this, ((InternalSyntax.LiteralExpressionSyntax)GreenNode).Token, Position, 0); }
        }
    }

    public class IdentifierNameSyntax : ExpressionSyntax
    {
        internal IdentifierNameSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxToken Identifier
        {
            get { return new SyntaxToken(this, ((InternalSyntax.IdentifierNameSyntax)GreenNode).Identifier, Position, 0); }
        }
    }

    public class BinaryExpressionSyntax : ExpressionSyntax
    {
        private ExpressionSyntax _left;
        private ExpressionSyntax _right;

        internal BinaryExpressionSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public ExpressionSyntax Left
        {
            get { return GetRed(ref _left, 0); }
        }

        public SyntaxToken OperatorToken
        {
            get { return new SyntaxToken(this, ((InternalSyntax.BinaryExpressionSyntax)GreenNode).OperatorToken, GetChildPosition(1), GetChildIndex(1)); }
        }

        public ExpressionSyntax Right
        {
            get { return GetRed(ref _right, 2); }
        }

        internal override SyntaxNode GetNodeSlot(int slot)
        {
            switch (slot)
            {
                case 0: return Left;
                case 2: return Right;
                default: return null;
            }
        }
    }

    public class VariableDeclarationSyntax : StatementSyntax
    {
        private ExpressionSyntax _expression;

        internal VariableDeclarationSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxToken VarToken
        {
            get { return new SyntaxToken(this, ((InternalSyntax.VariableDeclarationSyntax)GreenNode)._varKeyword, GetChildPosition(0), 0); }
        }

        public SyntaxToken Identifier
        {
            get { return new SyntaxToken(this, ((InternalSyntax.VariableDeclarationSyntax)GreenNode)._identifier, GetChildPosition(1), GetChildIndex(1)); }
        }

        public SyntaxToken EqualsToken
        {
            get { return new SyntaxToken(this, ((InternalSyntax.VariableDeclarationSyntax)GreenNode)._equalsToken, GetChildPosition(2), GetChildIndex(2)); }
        }

        public ExpressionSyntax Expression
        {
            get { return GetRed(ref _expression, 3); }
        }

        internal override SyntaxNode GetNodeSlot(int slot)
        {
            switch (slot)
            {
                case 3: return Expression;
                default: return null;
            }
        }
    }

    public class AssignmentSyntax : StatementSyntax
    {
        private ExpressionSyntax _expression;

        internal AssignmentSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxToken Identifier
        {
            get { return new SyntaxToken(this, ((InternalSyntax.AssignmentSyntax)GreenNode)._identifier, GetChildPosition(0), 0); }
        }

        public SyntaxToken EqualsToken
        {
            get { return new SyntaxToken(this, ((InternalSyntax.AssignmentSyntax)GreenNode)._equalsToken, GetChildPosition(1), GetChildIndex(1)); }
        }

        public ExpressionSyntax Expression
        {
            get { return GetRed(ref _expression, 2); }
        }

        internal override SyntaxNode GetNodeSlot(int slot)
        {
            switch (slot)
            {
                case 2: return Expression;
                default: return null;
            }
        }
    }

    public class ForStatementSyntax : StatementSyntax
    {
        private ExpressionSyntax _lower;
        private ExpressionSyntax _upper;
        private SyntaxList _statements;

        internal ForStatementSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxToken ForKeyword
        {
            get { return new SyntaxToken(this, ((InternalSyntax.ForStatementSyntax)GreenNode)._forKeyword, GetChildPosition(0), 0); }
        }

        public SyntaxToken Identifier
        {
            get { return new SyntaxToken(this, ((InternalSyntax.ForStatementSyntax)GreenNode)._identifier, GetChildPosition(1), GetChildIndex(1)); }
        }

        public SyntaxToken EqualsToken
        {
            get { return new SyntaxToken(this, ((InternalSyntax.ForStatementSyntax)GreenNode)._equalsToken, GetChildPosition(2), GetChildIndex(2)); }
        }

        public ExpressionSyntax Lower
        {
            get { return GetRed(ref _lower, 3); }
        }

        public SyntaxToken ToKeyword
        {
            get { return new SyntaxToken(this, ((InternalSyntax.ForStatementSyntax)GreenNode)._toKeyword, GetChildPosition(4), GetChildIndex(4)); }
        }

        public ExpressionSyntax Upper
        {
            get { return GetRed(ref _upper, 5); }
        }

        public SyntaxToken DoKeyword
        {
            get { return new SyntaxToken(this, ((InternalSyntax.ForStatementSyntax)GreenNode)._doKeyword, GetChildPosition(6), GetChildIndex(6)); }
        }

        public SyntaxList<StatementSyntax> Statements
        {
            get { return new SyntaxList<StatementSyntax>(GetRed(ref _statements, 7)); }
        }

        public SyntaxToken EndKeyword
        {
            get { return new SyntaxToken(this, ((InternalSyntax.ForStatementSyntax)GreenNode)._endKeyword, GetChildPosition(8), GetChildIndex(8)); }
        }

        internal override SyntaxNode GetNodeSlot(int slot)
        {
            switch (slot)
            {
                case 3: return Lower;
                case 5: return Upper;
                case 7: return GetRed(ref _statements, 7);
                default: return null;
            }
        }
    }

    public class ReadIntSyntax : StatementSyntax
    {
        internal ReadIntSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxToken ReadIntKeyword
        {
            get { return new SyntaxToken(this, ((InternalSyntax.ReadIntSyntax)GreenNode)._readIntKeyword, GetChildPosition(0), 0); }
        }

        public SyntaxToken Identifier
        {
            get { return new SyntaxToken(this, ((InternalSyntax.ReadIntSyntax)GreenNode)._identifier, GetChildPosition(1), GetChildIndex(1)); }
        }
    }

    /*
    public class ReadStringSyntax : StatementSyntax
    {
        public SyntaxToken ReadStringKeyword { get; }
        public SyntaxToken Identifier { get; }
    }
    */

    public class PrintStatementSyntax : StatementSyntax
    {
        private ExpressionSyntax _expression;

        internal PrintStatementSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxToken PrintKeyword
        {
            get { return new SyntaxToken(this, ((InternalSyntax.PrintStatementSyntax)GreenNode)._printKeyword, GetChildPosition(0), 0); }
        }

        public ExpressionSyntax Expression
        {
            get { return GetRed(ref _expression, 1); }
        }

        internal override SyntaxNode GetNodeSlot(int slot)
        {
            switch (slot)
            {
                case 1: return Expression;
                default: return null;
            }
        }
    }

    public class EmptyStatementSyntax : StatementSyntax
    {
        internal EmptyStatementSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxToken SemicolonToken
        {
            get { return new SyntaxToken(this, ((InternalSyntax.EmptyStatementSyntax)GreenNode).SemicolonToken, GetChildPosition(0), 0); }
        }
    }

    public class CompilationUnitSyntax : SyntaxNode
    {
        private SyntaxList _statements;

        internal CompilationUnitSyntax(InternalSyntax.SyntaxNode green, RedNode parent, int position) : base(green, parent, position)
        {
        }

        public SyntaxList<StatementSyntax> Statements
        {
            get { return new SyntaxList<StatementSyntax>(GetRed(ref _statements, 0)); }
        }

        public SyntaxToken EndOfFileToken
        {
            get { return new SyntaxToken(this, ((InternalSyntax.CompilationUnitSyntax)GreenNode)._endOfFileToken, GetChildPosition(1), GetChildIndex(1)); }
        }

        internal override SyntaxNode GetNodeSlot(int slot)
        {
            switch (slot)
            {
                case 0: return GetRed(ref _statements, 0);
                default: return null;
            }
        }
    }
}
