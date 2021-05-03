using SlothCodeAnalysis.Binder.Semantics;
using SlothCodeAnalysis.InternalUtilities;
using SlothCodeAnalysis.Syntax;
using SlothCodeAnalysis.BoundTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothCodeAnalysis.Errors;
using SlothCodeAnalysis.Symbols;

namespace SlothCodeAnalysis.Binder
{
    public partial class Binder
    {
        protected static bool IsSimpleBinaryOperator(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.AddExpression:
                case SyntaxKind.MultiplyExpression:
                case SyntaxKind.SubtractExpression:
                case SyntaxKind.DivideExpression:
                    return true;
            }
            return false;
        }

        private static BinaryOperatorKind SyntaxKindToBinaryOperatorKind(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.MultiplyExpression: return BinaryOperatorKind.Multiplication;
                case SyntaxKind.DivideExpression: return BinaryOperatorKind.Division;
                case SyntaxKind.AddExpression: return BinaryOperatorKind.Addition;
                case SyntaxKind.SubtractExpression: return BinaryOperatorKind.Subtraction;
                default: throw ExceptionUtilities.UnexpectedValue(kind);
            }
        }

        private BinaryOperatorAnalysisResult BinaryOperatorOverloadResolution(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, SyntaxNode node, BindingDiagnosticBag diagnostics)
        {
            var result = BinaryOperatorOverloadResolutionResult.GetInstance();
            this.OverloadResolution.BinaryOperatorOverloadResolution(kind, left, right, result);

            var possiblyBest = result.Best;

            result.Free();
            return possiblyBest;
        }

        private bool BindSimpleBinaryOperatorParts(BinaryExpressionSyntax node, BindingDiagnosticBag diagnostics, BoundExpression left, BoundExpression right, BinaryOperatorKind kind, out BinaryOperatorSignature signature, out BinaryOperatorAnalysisResult best)
        {
            bool foundOperator;
            best = this.BinaryOperatorOverloadResolution(kind, left, right, node, diagnostics);

            if (!best.HasValue)
            {
                signature = new BinaryOperatorSignature(kind, leftType: null, rightType: null, CreateErrorType());
                foundOperator = false;
            }
            else
            {
                signature = best.Signature;
                foundOperator = true;
            }

            return foundOperator;
        }

        internal TypeSymbol CreateErrorType()
        {
            return new TypeSymbol();
        }

        private static void ReportBinaryOperatorError(ExpressionSyntax node, BindingDiagnosticBag diagnostics, SyntaxToken operatorToken, BoundExpression left, BoundExpression right)
        {
            ErrorCode errorCode = ErrorCode.ERR_BadBinaryOps;
            Error(diagnostics, errorCode, node, operatorToken.Text, left.ToString(), right.ToString());
        }
    }
}
