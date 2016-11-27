using SlothCodeAnalysis.InternalUtilities;
using SlothCodeAnalysis.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    internal struct BinaryOperatorSignature : IEquatable<BinaryOperatorSignature>
    {
        public static BinaryOperatorSignature Error = default(BinaryOperatorSignature);

        public readonly TypeSymbol LeftType;
        public readonly TypeSymbol RightType;
        public readonly TypeSymbol ReturnType;
        public readonly BinaryOperatorKind Kind;

        public BinaryOperatorSignature(BinaryOperatorKind kind, TypeSymbol leftType, TypeSymbol rightType, TypeSymbol returnType)
        {
            this.Kind = kind;
            this.LeftType = leftType;
            this.RightType = rightType;
            this.ReturnType = returnType;
        }

        public override string ToString()
        {
            return $"kind: {this.Kind} left: {this.LeftType} right: {this.RightType} return: {this.ReturnType}";
        }

        public bool Equals(BinaryOperatorSignature other)
        {
            return
                this.Kind == other.Kind &&
                this.LeftType == other.LeftType &&
                this.RightType == other.RightType &&
                this.ReturnType == other.ReturnType;
        }

        public static bool operator ==(BinaryOperatorSignature x, BinaryOperatorSignature y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(BinaryOperatorSignature x, BinaryOperatorSignature y)
        {
            return !x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            return obj is BinaryOperatorSignature && Equals((BinaryOperatorSignature)obj);
        }

        public override int GetHashCode()
        {
            return Hash.Combine(ReturnType,
                   Hash.Combine(LeftType,
                   Hash.Combine(RightType, (int)Kind)));
        }
    }
}
