using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Diagnostic
{
    public class DiagnosticBag
    {
        // TODO: Change to use Concurrent Collections - but need to fix assembly targets first

        private IList<SlothDiagnostic> _bag = new List<SlothDiagnostic>();

        public void Add(SlothDiagnostic diagnostic)
        {
            _bag.Add(diagnostic);
        }

        // TODO: Need to remove this and implement diagnostic verification through better mechanism
        public ICollection<SlothDiagnostic> AsCollection()
        {
            return _bag;
        }
    }
}
