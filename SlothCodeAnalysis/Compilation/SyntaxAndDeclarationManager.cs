using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Compilation
{
    class SyntaxAndDeclarationManager
    {
        internal readonly SyntaxTree _externalSyntaxTree;

        internal SyntaxAndDeclarationManager(SyntaxTree externalSyntaxTree)
        {
            _externalSyntaxTree = externalSyntaxTree;
        }
    }
}
