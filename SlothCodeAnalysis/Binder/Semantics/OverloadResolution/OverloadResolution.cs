using SlothCodeAnalysis.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder.Semantics
{
    internal sealed partial class OverloadResolution
    {
        private readonly Binder _binder;

        public OverloadResolution(Binder binder)
        {
            _binder = binder;
        }

        private SlothCompilation Compilation
        {
            get { return _binder.Compilation; }
        }
    }
}