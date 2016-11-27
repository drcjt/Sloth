using SlothCodeAnalysis.Binder.Semantics;
using SlothCodeAnalysis.Symbols;
using SlothCodeAnalysis.Syntax;
using System.Collections.Immutable;
using System.Diagnostics;

namespace SlothCodeAnalysis.BoundTree
{
    public abstract partial class BoundExpression : BoundNode
    {
        protected BoundExpression(BoundKind kind, SyntaxNode syntax, TypeSymbol type) : base(kind, syntax)
        {
            this.Type = type;
        }

        public TypeSymbol Type { get; }
    }

    public abstract partial class BoundStatement : BoundNode
    {
        protected BoundStatement(BoundKind kind, SyntaxNode syntax) : base(kind, syntax)
        {
        }
    }

    public sealed partial class BoundIfStatement : BoundStatement
    {
        public BoundIfStatement(SyntaxNode syntax, /*ImmutableArray<LocalSymbol> locals,*/ BoundExpression condition, BoundStatement consequence, BoundStatement alternativeOpt)
            : base(BoundKind.IfStatement, syntax)
        {
            //this.Locals = locals;
            this.Condition = condition;
            this.Consequence = consequence;
            this.AlternativeOpt = alternativeOpt;
        }


        //public ImmutableArray<LocalSymbol> Locals { get; }

        public BoundExpression Condition { get; }
        public BoundStatement Consequence { get; }
        public BoundStatement AlternativeOpt { get; }

        public override BoundNode Accept(BoundTreeVisitor visitor)
        {
            return visitor.VisitIfStatement(this);
        }

        /*
        public BoundIfStatement Update(ImmutableArray<LocalSymbol> locals, BoundExpression condition, BoundStatement consequence, BoundStatement alternativeOpt)
        {
            if (locals != this.Locals || condition != this.Condition || consequence != this.Consequence || alternativeOpt != this.AlternativeOpt)
            {
                var result = new BoundIfStatement(this.Syntax, locals, condition, consequence, alternativeOpt, this.HasErrors);
                result.WasCompilerGenerated = this.WasCompilerGenerated;
                return result;
            }
            return this;
        }
        */
    }

    public sealed partial class BoundForStatement : BoundStatement
    {
        public BoundForStatement(SyntaxNode syntax, /* ImmutableArray<LocalSymbol> outerLocals, */ BoundStatement initializer, BoundExpression condition, BoundStatement increment, BoundStatement body)
            : base(BoundKind.ForStatement, syntax)
        {
            //this.OuterLocals = outerLocals;
            this.Initializer = initializer;
            this.Condition = condition;
            this.Increment = increment;
            this.Body = body;
        }

        //public ImmutableArray<LocalSymbol> OuterLocals { get; }

        public BoundStatement Initializer { get; }
        public BoundExpression Condition { get; }
        public BoundStatement Increment { get; }
        public BoundStatement Body { get; }

        public override BoundNode Accept(BoundTreeVisitor visitor)
        {
            return visitor.VisitForStatement(this);
        }

        /*
        public BoundForStatement Update(ImmutableArray<LocalSymbol> outerLocals, BoundStatement initializer, BoundExpression condition, BoundStatement increment, BoundStatement body, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel)
        {
            if (outerLocals != this.OuterLocals || initializer != this.Initializer || condition != this.Condition || increment != this.Increment || body != this.Body || breakLabel != this.BreakLabel || continueLabel != this.ContinueLabel)
            {
                var result = new BoundForStatement(this.Syntax, outerLocals, initializer, condition, increment, body, breakLabel, continueLabel, this.HasErrors);
                result.WasCompilerGenerated = this.WasCompilerGenerated;
                return result;
            }
            return this;
        }
        */
    }

    public partial class BoundStatementList : BoundStatement
    {
        protected BoundStatementList(BoundKind kind, SyntaxNode syntax, ImmutableArray<BoundStatement> statements)
            : base(kind, syntax)
        {
            this.Statements = statements;
        }

        public BoundStatementList(SyntaxNode syntax, ImmutableArray<BoundStatement> statements)
            : base(BoundKind.StatementList, syntax)
        {
            this.Statements = statements;
        }

        public ImmutableArray<BoundStatement> Statements { get; }

        public override BoundNode Accept(BoundTreeVisitor visitor)
        {
            return visitor.VisitStatementList(this);
        }

        /*
        public BoundStatementList Update(ImmutableArray<BoundStatement> statements)
        {
            if (statements != this.Statements)
            {
                var result = new BoundStatementList(this.Syntax, statements, this.HasErrors);
                result.WasCompilerGenerated = this.WasCompilerGenerated;
                return result;
            }
            return this;
        }
        */
    }

    public sealed partial class BoundBlock : BoundStatementList
    {
        public BoundBlock(SyntaxNode syntax, /*ImmutableArray<LocalSymbol> locals, ImmutableArray<LocalFunctionSymbol> localFunctions, */ ImmutableArray<BoundStatement> statements)
            : base(BoundKind.Block, syntax, statements)
        {
            //this.Locals = locals;
            //this.LocalFunctions = localFunctions;
        }

        //public ImmutableArray<LocalSymbol> Locals { get; }
        //public ImmutableArray<LocalFunctionSymbol> LocalFunctions { get; }

        public override BoundNode Accept(BoundTreeVisitor visitor)
        {
            return visitor.VisitBlock(this);
        }

        /*
        public BoundBlock Update(ImmutableArray<LocalSymbol> locals, ImmutableArray<LocalFunctionSymbol> localFunctions, ImmutableArray<BoundStatement> statements)
        {
            if (locals != this.Locals || localFunctions != this.LocalFunctions || statements != this.Statements)
            {
                var result = new BoundBlock(this.Syntax, locals, localFunctions, statements, this.HasErrors);
                result.WasCompilerGenerated = this.WasCompilerGenerated;
                return result;
            }
            return this;
        }
        */
    }

    public sealed partial class BoundLiteral : BoundExpression
    {
        public BoundLiteral(SyntaxNode syntax, ConstantValue constantValueOpt, TypeSymbol type)
            : base(BoundKind.Literal, syntax, type)
        {
            this.ConstantValueOpt = constantValueOpt;
        }

        public ConstantValue ConstantValueOpt { get; }

        public override BoundNode Accept(BoundTreeVisitor visitor)
        {
            return visitor.VisitLiteral(this);
        }

        /*
        public BoundLiteral Update(ConstantValue constantValueOpt, TypeSymbol type)
        {
            if (constantValueOpt != this.ConstantValueOpt || type != this.Type)
            {
                var result = new BoundLiteral(this.Syntax, constantValueOpt, type, this.HasErrors);
                result.WasCompilerGenerated = this.WasCompilerGenerated;
                return result;
            }
            return this;
        }
        */
    }

    public sealed partial class BoundBinaryOperator : BoundExpression
    {
        public BoundBinaryOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression left, BoundExpression right, TypeSymbol type)
            : base(BoundKind.BinaryOperator, syntax, type)
        {

            Debug.Assert(left != null, "Field 'left' cannot be null (use Null=\"allow\" in BoundNodes.xml to remove this check)");
            Debug.Assert(right != null, "Field 'right' cannot be null (use Null=\"allow\" in BoundNodes.xml to remove this check)");
            Debug.Assert(type != null, "Field 'type' cannot be null (use Null=\"allow\" in BoundNodes.xml to remove this check)");

            this.OperatorKind = operatorKind;
            this.Left = left;
            this.Right = right;
        }


        public BinaryOperatorKind OperatorKind { get; }

        public BoundExpression Left { get; }

        public BoundExpression Right { get; }

        public override BoundNode Accept(BoundTreeVisitor visitor)
        {
            return visitor.VisitBinaryOperator(this);
        }

        /*
        public BoundBinaryOperator Update(BinaryOperatorKind operatorKind, BoundExpression left, BoundExpression right, ConstantValue constantValueOpt, MethodSymbol methodOpt, LookupResultKind resultKind, TypeSymbol type)
        {
            if (operatorKind != this.OperatorKind || left != this.Left || right != this.Right || constantValueOpt != this.ConstantValueOpt || methodOpt != this.MethodOpt || resultKind != this.ResultKind || type != this.Type)
            {
                var result = new BoundBinaryOperator(this.Syntax, operatorKind, left, right, constantValueOpt, methodOpt, resultKind, type, this.HasErrors);
                result.WasCompilerGenerated = this.WasCompilerGenerated;
                return result;
            }
            return this;
        }
        */
    }
}
