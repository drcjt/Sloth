using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    internal sealed class BinaryOperatorOverloadResolutionResult
    {
        public readonly IList<BinaryOperatorAnalysisResult> Results;

        private BinaryOperatorOverloadResolutionResult()
        {
            this.Results = new List<BinaryOperatorAnalysisResult>(10);
        }

        public bool AnyValid()
        {
            foreach (var result in Results)
            {
                if (result.IsValid)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetValidCount()
        {
            int count = 0;
            foreach (var result in Results)
            {
                if (result.IsValid)
                {
                    count++;
                }
            }

            return count;
        }

        public BinaryOperatorAnalysisResult Best
        {
            get
            {
                BinaryOperatorAnalysisResult best = default(BinaryOperatorAnalysisResult);
                foreach (var result in Results)
                {
                    if (result.IsValid)
                    {
                        if (best.IsValid)
                        {
                            // More than one best applicable method
                            return default(BinaryOperatorAnalysisResult);
                        }
                        best = result;
                    }
                }

                return best;
            }
        }

        #region "Poolable"
        public static BinaryOperatorOverloadResolutionResult GetInstance()
        {
            return new BinaryOperatorOverloadResolutionResult();
        }

        public void Free()
        {
        }
        #endregion
    }
}
