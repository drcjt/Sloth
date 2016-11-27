using SlothCodeAnalysis.BoundTree;
using SlothCodeAnalysis.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    internal sealed partial class OverloadResolution
    {
        internal static class BinopEasyOut
        {
            private const BinaryOperatorKind ERR = BinaryOperatorKind.Error;
            private const BinaryOperatorKind STR = BinaryOperatorKind.String;
            private const BinaryOperatorKind INT = BinaryOperatorKind.Int;

            // Overload resolution for Y * / - % < > <= >= X
            private static readonly BinaryOperatorKind[,] s_arithmetic =
            {
                //                    ----------------regular-------------------
                //          str  i32  
                /*  str */
                      { ERR, ERR },
                /*  i32 */
                      { ERR, INT },
            };

            // Overload resolution for Y + X
            private static readonly BinaryOperatorKind[,] s_addition =
            {
                //                    ----------------regular-------------------
                //          str  i32
                /*  str */
                      { STR, ERR },
                /*  i32 */
                      { ERR, INT },
            };

            private static readonly BinaryOperatorKind[][,] s_opkind =
            {
                /* *  */ s_arithmetic,
                /* +  */ s_addition,
                /* -  */ s_arithmetic,
                /* /  */ s_arithmetic,
            };

            private static int? TypeToIndex(TypeSymbol type)
            {
                switch (type.GetSpecialTypeSafe())
                {
                    case SpecialType.System_String: return 0;
                    case SpecialType.System_Int32: return 1;

                    default: return null;
                }
            }

            public static BinaryOperatorKind OpKind(BinaryOperatorKind kind, TypeSymbol left, TypeSymbol right)
            {
                int? leftIndex = TypeToIndex(left);
                if (leftIndex == null)
                {
                    return BinaryOperatorKind.Error;
                }
                int? rightIndex = TypeToIndex(right);
                if (rightIndex == null)
                {
                    return BinaryOperatorKind.Error;
                }

                var result = BinaryOperatorKind.Error;

                result = s_opkind[kind.OperatorIndex()][leftIndex.Value, rightIndex.Value];

                return result == BinaryOperatorKind.Error ? result : result | kind;
            }
        }

        private void BinaryOperatorEasyOut(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, BinaryOperatorOverloadResolutionResult result)
        {
            var leftType = left.Type;
            if ((object)leftType == null)
            {
                return;
            }

            var rightType = right.Type;
            if ((object)rightType == null)
            {
                return;
            }

            var easyOut = BinopEasyOut.OpKind(kind, leftType, rightType);

            if (easyOut == BinaryOperatorKind.Error)
            {
                return;
            }

            BinaryOperatorSignature signature = this.Compilation.builtInOperators.GetSignature(easyOut);

            result.Results.Add(BinaryOperatorAnalysisResult.Applicable(signature));
        }
    }
}
