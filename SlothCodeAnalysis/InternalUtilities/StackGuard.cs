using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.InternalUtilities
{
    internal static class StackGuard
    {
        public static bool IsInsufficientExecutionStackException(Exception ex)
        {
            return ex.GetType().Name == "InsufficientExecutionStackException";
        }
    }
}
