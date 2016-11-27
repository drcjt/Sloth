using SlothCodeAnalysis.BoundTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    internal sealed partial class OverloadResolution
    {
        public void BinaryOperatorOverloadResolution(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, BinaryOperatorOverloadResolutionResult result)
        {
            Debug.Assert(left != null);
            Debug.Assert(right != null);
            Debug.Assert(result.Results.Count == 0);

            BinaryOperatorEasyOut(kind, left, right, result);
            if (result.Results.Count > 0)
            {
                return;
            }        
        }
    }
}
