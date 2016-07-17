using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Symbols
{
    internal class Symbol : ISymbol
    {
        private readonly SyntaxToken _identifierToken;

        public string Name
        {
            get
            {
                return _identifierToken.Text;
            }
        }

        public bool IsDefinition
        {
            get
            {
                // TODO: work out how this should work!
                return false;
            }
        }

        private TypeKind _typeKind;
        public TypeKind TypeKind
        {
            get
            {
                return _typeKind;
            }
        }

        public static Symbol MakeSymbol()
        {
            return new Symbol();
        }
    }
}
