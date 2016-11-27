using SlothCodeAnalysis.InternalUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    internal struct BinaryOperatorAnalysisResult
    {
        public readonly BinaryOperatorSignature Signature;
        public readonly OperatorAnalysisResultKind Kind;

        private BinaryOperatorAnalysisResult(OperatorAnalysisResultKind kind, BinaryOperatorSignature signature)
        {
            this.Kind = kind;
            this.Signature = signature;
        }

        public bool IsValid
        {
            get { return this.Kind == OperatorAnalysisResultKind.Applicable; }
        }

        public bool HasValue
        {
            get { return this.Kind != OperatorAnalysisResultKind.Undefined; }
        }

        public override bool Equals(object obj)
        {
            // implement if needed
            throw ExceptionUtilities.Unreachable;
        }

        public override int GetHashCode()
        {
            // implement if needed
            throw ExceptionUtilities.Unreachable;
        }

        public static BinaryOperatorAnalysisResult Applicable(BinaryOperatorSignature signature)
        {
            return new BinaryOperatorAnalysisResult(OperatorAnalysisResultKind.Applicable, signature);
        }

        public static BinaryOperatorAnalysisResult Inapplicable(BinaryOperatorSignature signature)
        {
            return new BinaryOperatorAnalysisResult(OperatorAnalysisResultKind.Inapplicable, signature);
        }
    }
}
