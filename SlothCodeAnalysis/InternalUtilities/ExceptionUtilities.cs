using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.InternalUtilities
{
    internal static class ExceptionUtilities
    {
        internal static Exception UnexpectedValue(object o)
        {
            string output = string.Format("Unexpected value '{0}' of type '{1}'", o, (o != null) ? o.GetType().FullName : "<unknown>");
            return new InvalidOperationException(output);
        }

        internal static Exception Unreachable
        {
            get
            {
                return new InvalidOperationException("This program location is thought to be unreachable.");
            }
        }
    }
}
