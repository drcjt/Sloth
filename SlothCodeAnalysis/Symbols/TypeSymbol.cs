using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Symbols
{
    public class TypeSymbol
    {
        public virtual SpecialType SpecialType
        {
            get
            {
                return SpecialType.None;
            }
        }
    }
}
