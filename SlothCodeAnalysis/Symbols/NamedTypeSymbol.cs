using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Symbols
{
    internal class NamedTypeSymbol : TypeSymbol
    {
        public string Name { get; }
        private SpecialType _specialType;
        internal NamedTypeSymbol(string name, SpecialType SpecialType)
        {
            Name = name;
            _specialType = SpecialType;
        }

        public static readonly NamedTypeSymbol Int32 = new NamedTypeSymbol("Int32", SpecialType.System_Int32);
        public static readonly NamedTypeSymbol String = new NamedTypeSymbol("String", SpecialType.System_String);

        public override SpecialType SpecialType
        {
            get
            {
                return _specialType;
            }
        }
    }
}
