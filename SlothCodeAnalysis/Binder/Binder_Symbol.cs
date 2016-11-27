using SlothCodeAnalysis.Compilation;
using SlothCodeAnalysis.Symbols;
using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Binder
{
    public partial class Binder
    {
        internal NamedTypeSymbol GetSpecialType(SpecialType typeId, SyntaxNode node)
        {
            return GetSpecialType(Compilation, typeId, node);
        }

        internal static NamedTypeSymbol GetSpecialType(SlothCompilation compilation, SpecialType typeId, SyntaxNode node)
        {
            NamedTypeSymbol typeSymbol = compilation.GetSpecialType(typeId);
            return typeSymbol;
        }
    }
}
