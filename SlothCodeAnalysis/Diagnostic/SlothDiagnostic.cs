using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Diagnostic
{
    public sealed class SlothDiagnostic
    {
        private readonly DiagnosticInfo _info;
        private readonly SourceLocation _location;

        internal SlothDiagnostic(DiagnosticInfo info, SourceLocation location)
        {
            _info = info;
            _location = location;
        }
    }
}
