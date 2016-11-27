using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis
{
    internal static class SpecialTypeExtensions
    {
        public static SpecialType FromRuntimeTypeOfLiteralValue(object value)
        {
            // Perf: Note that JIT optimizes each expression val.GetType() == typeof(T) to a single register comparison.
            // Also the checks are sorted by commonality of the checked types.

            if (value.GetType() == typeof(string))
            {
                return SpecialType.System_String;
            }

            if (value.GetType() == typeof(int))
            {
                return SpecialType.System_Int32;
            }

            return SpecialType.None;
        }
    }
}
