using SlothCodeAnalysis.Binder.Semantics;
using SlothCodeAnalysis.Symbols;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Compilation
{
    internal class BuiltInOperators
    {
        private readonly SlothCompilation _compilation;

        internal BuiltInOperators(SlothCompilation compilation)
        {
            _compilation = compilation;
        }

        internal BinaryOperatorSignature GetSignature(BinaryOperatorKind kind)
        {
            var left = LeftType(kind);
            switch (kind.Operator())
            {
                case BinaryOperatorKind.Multiplication:
                case BinaryOperatorKind.Division:
                case BinaryOperatorKind.Subtraction:
                    return new BinaryOperatorSignature(kind, left, left, left);
                case BinaryOperatorKind.Addition:
                    return new BinaryOperatorSignature(kind, LeftType(kind), RightType(kind), ReturnType(kind));
            }
            return new BinaryOperatorSignature(kind, LeftType(kind), RightType(kind), ReturnType(kind));
        }

        private TypeSymbol LeftType(BinaryOperatorKind kind)
        {
            switch (kind.OperandTypes())
            {
                case BinaryOperatorKind.Int: return _compilation.GetSpecialType(SpecialType.System_Int32);
                case BinaryOperatorKind.String:
                    return _compilation.GetSpecialType(SpecialType.System_String);
            }
            Debug.Assert(false, "Bad operator kind in left type");
            return null;
        }

        private TypeSymbol RightType(BinaryOperatorKind kind)
        {
            switch (kind.OperandTypes())
            {
                case BinaryOperatorKind.Int: return _compilation.GetSpecialType(SpecialType.System_Int32);
                case BinaryOperatorKind.String:
                    return _compilation.GetSpecialType(SpecialType.System_String);
            }
            Debug.Assert(false, "Bad operator kind in right type");
            return null;
        }

        private TypeSymbol ReturnType(BinaryOperatorKind kind)
        {
            switch (kind.OperandTypes())
            {
                case BinaryOperatorKind.Int: return _compilation.GetSpecialType(SpecialType.System_Int32);
                case BinaryOperatorKind.String:
                    return _compilation.GetSpecialType(SpecialType.System_String);
            }
            Debug.Assert(false, "Bad operator kind in return type");
            return null;
        }
    }
}
