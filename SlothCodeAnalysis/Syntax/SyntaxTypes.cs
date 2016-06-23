namespace SlothCodeAnalysis.Syntax
{
    public class StatementSyntax
    {

    }

    public class ExpressionSyntax
    {

    }

    public class LiteralExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken Token { get; }
    }

    public class VariableSyntax : ExpressionSyntax
    {
        public SyntaxToken VariableIdentifier { get; }
    }

    public class BinaryExpressionSyntax : ExpressionSyntax
    {

        public ExpressionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Right { get; }
    }

    public class VariableDeclarationSyntax : StatementSyntax
    {
        public SyntaxToken VarToken { get; }
        public SyntaxToken Identifier { get; }
        public SyntaxToken EqualsToken { get; }
        public ExpressionSyntax Expression { get; }
    }

    public class AssignmentSyntax : StatementSyntax
    {
        public SyntaxToken Identifier { get; }
        public SyntaxToken EqualsToken { get; }
        public ExpressionSyntax Expression { get; }
    }

    public class ForStatementSyntax
    {
        public SyntaxToken ForKeyword { get; }
        public SyntaxToken Identifier { get; }
        public SyntaxToken EqualsToken { get; }
        public ExpressionSyntax Lower { get; }
        public SyntaxToken ToKeyword { get; }
        public ExpressionSyntax Upper { get; }
        public SyntaxToken DoKeyword { get; }
        public StatementSyntax Statement { get; }
        public SyntaxToken EndKeyword { get; }
    }

    public class ReadIntSyntax : StatementSyntax
    {
        public SyntaxToken ReadIntKeyword { get; }
        public SyntaxToken Identifier { get; }
    }

    public class ReadStringSyntax : StatementSyntax
    {
        public SyntaxToken ReadStringKeyword { get; }
        public SyntaxToken Identifier { get; }
    }

    public class SequenceSyntax : StatementSyntax
    {
        public StatementSyntax First { get; }
        public StatementSyntax Second { get; }
    }

    public class PrintStatementSyntax : StatementSyntax
    {
        public SyntaxToken PrintKeyword { get; }
        public ExpressionSyntax Expression { get; }
    }
}
