using SlothCodeAnalysis.Symbols;
using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Compilation
{
    public class SlothCompilation
    {
        private readonly SyntaxAndDeclarationManager _syntaxAndDeclaration;

        public SlothCompilation(SyntaxTree syntaxTree)
        {
            _syntaxAndDeclaration = new SyntaxAndDeclarationManager(syntaxTree);
            builtInOperators = new BuiltInOperators(this);
        }

        public static SlothCompilation Create(SyntaxTree syntaxTree)
        {
            return new SlothCompilation(syntaxTree);
        }

        public SyntaxTreeSemanticModel GetSemanticModel(SyntaxTree syntaxTree)
        {
            return new SyntaxTreeSemanticModel(this, syntaxTree);
        }

        /// <summary>
        /// Emit the assembly for the compiled source code into the specified stream
        /// </summary>
        /// <param name="asmStream"></param>
        /// <returns></returns>
        public bool Emit(Stream asmStream)
        {
            return false;
        }

        /// <summary>
        /// Get the symbol for the predefined type from the COR Library referenced by this compilation.
        /// </summary>
        internal NamedTypeSymbol GetSpecialType(SpecialType specialType)
        {
            if (specialType <= SpecialType.None || specialType > SpecialType.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(specialType));
            }

            switch (specialType)
            {
                case SpecialType.System_Int32: return NamedTypeSymbol.Int32;
                case SpecialType.System_String: return NamedTypeSymbol.String;
                default: throw new Exception("Unknown Special Type");
            }
        }

        internal readonly BuiltInOperators builtInOperators;
    }
}
