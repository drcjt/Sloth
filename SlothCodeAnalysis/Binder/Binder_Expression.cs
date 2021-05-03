using SlothCodeAnalysis.Binder.Semantics;
using SlothCodeAnalysis.BoundTree;
using SlothCodeAnalysis.InternalUtilities;
using SlothCodeAnalysis.Symbols;
using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder
{
    /// <summary>
    /// This portion of the binder converts an <see cref="ExpressionSyntax"/> into a <see cref="BoundExpression"/>.
    /// </summary>
    public partial class Binder
    {

        /// <summary>
        /// Bind the expression and verify the expression matches the combination of lvalue and
        /// rvalue requirements given by valueKind. If the expression was bound successfully, but
        /// did not meet the requirements, the return value will be a <see cref="BoundBadExpression"/> that
        /// (typically) wraps the subexpression.
        /// </summary>
        internal BoundExpression BindValue(ExpressionSyntax node, BindingDiagnosticBag diagnostics, BindValueKind valueKind)
        {
            var result = BindExpression(node, diagnostics, invoked: false, indexed: false);
            return CheckValue(result, valueKind);
        }

        public BoundExpression BindExpression(ExpressionSyntax node, BindingDiagnosticBag diagnostics)
        {
            return BindExpression(node, diagnostics, invoked: false, indexed: false);
        }

        protected BoundExpression BindExpression(ExpressionSyntax node, BindingDiagnosticBag diagnostics, bool invoked, bool indexed)
        {
            BoundExpression expr = BindExpressionInternal(node, diagnostics, invoked, indexed);
            return expr;
        }

        private BoundExpression BindExpressionInternal(ExpressionSyntax node, BindingDiagnosticBag diagnostics, bool invoked, bool indexed)
        {
            switch (node.Kind)
            {
                /*case SyntaxKind.IdentifierName:
                    return BindIdentifier((SimpleNameSyntax)node, invoked, diagnostics);
                    */
                case SyntaxKind.AddExpression:
                case SyntaxKind.MultiplyExpression:
                case SyntaxKind.SubtractExpression:
                case SyntaxKind.DivideExpression:
                    return BindSimpleBinaryOperator((BinaryExpressionSyntax)node, diagnostics);


                case SyntaxKind.NumericLiteralExpression:
                case SyntaxKind.StringLiteralExpression:
                    return BindLiteralConstant((LiteralExpressionSyntax)node);

                default:
                    Debug.Assert(false, "Unexpected SyntaxKind " + node.Kind);
                    throw new Exception("Bad expression " + node);
            }
        }

        /*
        /// <summary>
        /// Binds a simple identifier.
        /// </summary>
        private BoundExpression BindIdentifier(SimpleNameSyntax node, bool invoked)
        {
        }
        */

        private BoundExpression BindSimpleBinaryOperator(BinaryExpressionSyntax node, BindingDiagnosticBag diagnostics)
        {
            // The simple binary operators are left-associative, and expressions of the form
            // a + b + c + d .... are relatively common in machine-generated code. The parser can handle
            // creating a deep-on-the-left syntax tree no problem, and then we promptly blow the stack during
            // semantic analysis. Here we build an explicit stack to handle the left-hand recursion.

            Debug.Assert(IsSimpleBinaryOperator(node.Kind));

            var expressions = new Stack<BoundExpression>();
            var syntaxNodes = new Stack<BinaryExpressionSyntax>();       

            ExpressionSyntax current = node;
            while (IsSimpleBinaryOperator(current.Kind))
            {
                var binOp = (BinaryExpressionSyntax)current;
                syntaxNodes.Push(binOp);
                expressions.Push(BindValue(binOp.Right, diagnostics, BindValueKind.RValue));
                current = binOp.Left;
            }
            BoundExpression leftMost = BindExpression(current, diagnostics);
            expressions.Push(leftMost);

            Debug.Assert(syntaxNodes.Count + 1 == expressions.Count);
            int compoundStringLength = 0;

            while (syntaxNodes.Count > 0)
            {
                BinaryExpressionSyntax syntaxNode = syntaxNodes.Pop();
                BoundExpression left = expressions.Pop();
                BoundExpression right = expressions.Pop();
                left = CheckValue(left, BindValueKind.RValue);
                BoundExpression boundOp = BindSimpleBinaryOperator(syntaxNode, diagnostics, left, right, ref compoundStringLength);
                expressions.Push(boundOp);
            }

            Debug.Assert(expressions.Count == 1);

            var result = expressions.Peek();
            return result;
        }

        private BoundExpression BindSimpleBinaryOperator(BinaryExpressionSyntax node, BindingDiagnosticBag diagnostics, BoundExpression left, BoundExpression right, ref int compoundStringLength)
        {
            BinaryOperatorKind kind = SyntaxKindToBinaryOperatorKind(node.Kind);

            TypeSymbol leftType = left.Type;
            TypeSymbol rightType = right.Type;

            /* Perform binary operator overload resoluton */
            BinaryOperatorSignature signature;
            BinaryOperatorAnalysisResult best;
            bool foundOperator = BindSimpleBinaryOperatorParts(node, diagnostics, left, right, kind, out signature, out best);

            if (!foundOperator)
            {
                ReportBinaryOperatorError(node, diagnostics, node.OperatorToken, left, right);
            }

            BoundExpression resultLeft = left;
            BoundExpression resultRight = right;
            BinaryOperatorKind resultOperatorKind = signature.Kind;
            TypeSymbol resultType = signature.ReturnType;

            return new BoundBinaryOperator(
                node,
                resultOperatorKind,
                resultLeft,
                resultRight,
                resultType);
        }

        private BoundLiteral BindLiteralConstant(LiteralExpressionSyntax node)
        {
            var value = node.Token.Value;

            ConstantValue cv;
            TypeSymbol type = null;

            if (value == null)
            {
                throw new Exception("Cannot have null literal constants");
            }
            else
            {
                var specialType = SpecialTypeExtensions.FromRuntimeTypeOfLiteralValue(value);

                cv = ConstantValue.Create(value, specialType);
                type = GetSpecialType(specialType, node);
            }

            return new BoundLiteral(node, cv, type);
        }

        /// <summary>
        /// Check the expression is of the required lvalue and rvalue specified by valueKind.
        /// The method returns the original expression if the expression is of the required
        /// type. Otherwise, an appropriate error is added to the diagnostics bag and the
        /// method returns a BoundBadExpression node. The method returns the original
        /// expression without generating any error if the expression has errors.
        /// </summary>
        private BoundExpression CheckValue(BoundExpression expr, BindValueKind valueKind)
        {
            if (CheckValueKind(expr, valueKind))
            {
                return expr;
            }

            /*
            var resultKind = (valueKind == BindValueKind.RValue) ?
                LookupResultKind.NotAValue :
                LookupResultKind.NotAVariable;

            return ToBadExpression(expr, resultKind);
            */
            throw new NotImplementedException("TBD");
        }


        private static bool RequiresGettingValue(BindValueKind kind)
        {
            switch (kind)
            {
                case BindValueKind.RValue:
                    return true;

                case BindValueKind.Assignment:
                    return false;

                default:
                    throw ExceptionUtilities.UnexpectedValue(kind);
            }
        }

        private static bool RequiresSettingValue(BindValueKind kind)
        {
            switch (kind)
            {
                case BindValueKind.RValue:
                    return false;

                case BindValueKind.Assignment:
                    return true;

                default:
                    throw ExceptionUtilities.UnexpectedValue(kind);
            }
        }
    }
}
