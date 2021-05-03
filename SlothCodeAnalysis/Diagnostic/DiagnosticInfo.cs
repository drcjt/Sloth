using SlothCodeAnalysis.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Diagnostic
{
    internal class DiagnosticInfo
    {
        private readonly int _errorCode;
        private readonly object[] _arguments;

        internal DiagnosticInfo(ErrorCode errorCode, params object[] arguments)
        {
            _errorCode = (int)errorCode;
            _arguments = arguments;
        }
    }
}
