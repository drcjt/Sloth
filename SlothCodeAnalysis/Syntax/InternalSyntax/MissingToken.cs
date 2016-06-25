using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class MissingToken : SyntaxToken
    {
        internal MissingToken(SyntaxKind kind, string leading, string trailing) : base(kind, leading, trailing)
        {
        }
    }
}
