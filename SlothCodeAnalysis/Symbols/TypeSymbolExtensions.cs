using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Symbols
{
    internal static partial class TypeSymbolExtensions
    {
        public static SpecialType GetSpecialTypeSafe(this TypeSymbol type)
        {
            return (object)type != null ? type.SpecialType : SpecialType.None;
        }
    }
}
