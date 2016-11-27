using SlothCodeAnalysis.Binder.Semantics;
using SlothCodeAnalysis.InternalUtilities;
using SlothCodeAnalysis.Syntax;
using SlothCodeAnalysis.BoundTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private BinaryOperatorAnalysisResult BinaryOperatorOverloadResolution(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, SyntaxNode node)
        {
            var result = BinaryOperatorOverloadResolutionResult.GetInstance();
            //HashSet<DiagnosticInfo> useSiteDiagnostics = null;
            this.OverloadResolution.BinaryOperatorOverloadResolution(kind, left, right, result);

            var possiblyBest = result.Best;

            if (!result.Results.Any())
            {
                throw new Exception("Binary operator overload resolution failure");

                /*
                originalUserDefinedOperators = ImmutableArray<MethodSymbol>.Empty;
                resultKind = possiblyBest.HasValue ? LookupResultKind.Viable : LookupResultKind.Empty;
                */
            }

            /*
            if (possiblyBest.HasValue &&
                (object)possiblyBest.Signature.Method != null)
            {
                Symbol symbol = possiblyBest.Signature.Method;
                ReportDiagnosticsIfObsolete(diagnostics, symbol, node, hasBaseReceiver: false);
            }
            */

            result.Free();
            return possiblyBest;
        }
    }
}
