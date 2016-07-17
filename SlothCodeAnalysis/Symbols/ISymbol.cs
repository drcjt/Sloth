using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Symbols
{
    public interface ISymbol
    {
        string Name { get; }

        bool IsDefinition { get; }

        TypeKind TypeKind { get; }
    }
}
