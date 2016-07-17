using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Compilation
{
    class Compilation
    {
        private readonly SyntaxAndDeclarationManager _syntaxAndDeclaration;

        public Compilation(SyntaxTree syntaxTree)
        {
            _syntaxAndDeclaration = new SyntaxAndDeclarationManager(syntaxTree);
        }

        public static Compilation Create(SyntaxTree syntaxTree)
        {
            return new Compilation(syntaxTree);
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
    }
}
