using SlothCodeAnalysis.Diagnostic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder
{
    public class BindingDiagnosticBag
    {
        public static readonly BindingDiagnosticBag Discarded = new BindingDiagnosticBag(null);

        public readonly DiagnosticBag DiagnosticBag;

        public BindingDiagnosticBag()
        {
            DiagnosticBag = new DiagnosticBag();
        }

        public BindingDiagnosticBag(DiagnosticBag diagnosticBag)
        {
            DiagnosticBag = diagnosticBag;
        }

        internal void Add(SlothDiagnostic diagnostic)
        {
            DiagnosticBag?.Add(diagnostic);
        }
    }
}
