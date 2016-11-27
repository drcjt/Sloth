using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    internal enum OperatorAnalysisResultKind : byte
    {
        Undefined = 0,
        Inapplicable,
        Applicable,
    }
}
