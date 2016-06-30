using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax
{
    public enum SyntaxWalkerDepth : int
    {
        /// <summary>
        /// descend into only nodes
        /// </summary>
        Node = 0,

        /// <summary>
        /// descend into nodes and tokens
        /// </summary>
        Token = 1,
    }
}
