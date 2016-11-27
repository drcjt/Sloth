using SlothCodeAnalysis.Binder.Semantics;
using SlothCodeAnalysis.Compilation;
using System.Diagnostics;
using System.Threading;

namespace SlothCodeAnalysis.Binder
{
    /// <summary>
    /// A Binder converts names in to symbols and syntax nodes into bound trees. It is context
    /// dependent, relative to a location in source code.
    /// </summary>
    public partial class Binder
    {
        public SlothCompilation Compilation { get; }

        /// <summary>
        /// Used to create a root binder.
        /// </summary>
        public Binder(SlothCompilation compilation)
        {
            Debug.Assert(compilation != null);
            this.Compilation = compilation;
        }

        private OverloadResolution _lazyOverloadResolution;
        internal OverloadResolution OverloadResolution
        {
            get
            {
                if (_lazyOverloadResolution == null)
                {
                    Interlocked.CompareExchange(ref _lazyOverloadResolution, new OverloadResolution(this), null);
                }

                return _lazyOverloadResolution;
            }
        }


        internal enum BindValueKind : byte
        {
            /// <summary>
            /// Expression is the RHS of an assignment operation.
            /// </summary>
            /// <remarks>
            /// The following are rvalues: values, variables.
            /// </remarks>
            RValue,

            /// <summary>
            /// Expression is the LHS of a simple assignment operation.
            /// </summary>
            Assignment,
        }
    }
}
