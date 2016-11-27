using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    internal static partial class OperatorKindExtensions
    {
        public static int OperatorIndex(this BinaryOperatorKind kind)
        {
            return ((int)kind.Operator() >> 8) - 16;
        }

        public static BinaryOperatorKind Operator(this BinaryOperatorKind kind)
        {
            return kind & BinaryOperatorKind.OpMask;
        }

        public static BinaryOperatorKind OperandTypes(this BinaryOperatorKind kind)
        {
            return kind & BinaryOperatorKind.TypeMask;
        }
    }
}
