using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    [Flags]
    internal enum UnaryOperatorKind
    {
        // NOTE: these types should line up with the elements in BinaryOperatorKind
        TypeMask = 0x000000FF,

        Int = 0x00000015,
        _String = 0x0000001F, // reserved for binary op

        Error = 0x00000000,
    }

    public enum BinaryOperatorKind
    {
        // NOTE: these types should line up with the elements in UnaryOperatorKind
        TypeMask = UnaryOperatorKind.TypeMask,

        Int = UnaryOperatorKind.Int,
        String = UnaryOperatorKind._String,

        OpMask = 0x0000FF00,
        Multiplication = 0x00001000,
        Addition = 0x00001100,
        Subtraction = 0x00001200,
        Division = 0x00001300,

        Error = 0x00000000,

        IntMultiplication = Int | Multiplication,

        IntDivision = Int | Division,

        IntAddition = Int | Addition,
        StringConcatenation = String | Addition,

        IntSubtraction = Int | Subtraction,
    }
}
