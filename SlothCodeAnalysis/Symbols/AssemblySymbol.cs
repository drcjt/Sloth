using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Symbols
{
    /// <summary>
    /// Represents a .NET assembly, consisting of one or more modules.
    /// </summary>
    internal abstract class AssemblySymbol : Symbol
    {
        /// <summary>
        /// The system assembly, which provides primitive types like Object, String, etc., e.g. mscorlib.dll. 
        /// The value is provided by ReferenceManager and must not be modified. For SourceAssemblySymbol, non-missing 
        /// coreLibrary must match one of the referenced assemblies returned by GetReferencedAssemblySymbols() method of 
        /// the main module. If there is no existing assembly that can be used as a source for the primitive types, 
        /// the value is a Compilation.MissingCorLibrary. 
        /// </summary>
        private AssemblySymbol _corLibrary;

        /// <summary>
        /// The system assembly, which provides primitive types like Object, String, etc., e.g. mscorlib.dll. 
        /// The value is MissingAssemblySymbol if none of the referenced assemblies can be used as a source for the 
        /// primitive types and the owning assembly cannot be used as the source too. Otherwise, it is one of 
        /// the referenced assemblies returned by GetReferencedAssemblySymbols() method or the owning assembly.
        /// </summary>
        internal AssemblySymbol CorLibrary
        {
            get
            {
                return _corLibrary;
            }
        }

        /// <summary>
        /// Lookup declaration for predefined CorLib type in this Assembly.
        /// </summary>
        /// <returns>The symbol for the pre-defined type or an error type if the type is not defined in the core library.</returns>
        internal abstract NamedTypeSymbol GetDeclaredSpecialType(SpecialType type);

        /// <summary>
        /// Gets the symbol for the pre-defined type from core library associated with this assembly.
        /// </summary>
        /// <returns>The symbol for the pre-defined type or an error type if the type is not defined in the core library.</returns>
        internal NamedTypeSymbol GetSpecialType(SpecialType type)
        {
            return CorLibrary.GetDeclaredSpecialType(type);
        }    
    }
}
