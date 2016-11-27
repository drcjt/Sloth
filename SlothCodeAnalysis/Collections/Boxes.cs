using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Collections
{
    internal static class Boxes
    {
        public static readonly object BoxedInt32Zero = 0;
        public static readonly object BoxedInt32One = 1;

        private static readonly object[] s_boxedAsciiChars = new object[128];

        public static object Box(int i)
        {
            switch (i)
            {
                case 0: return BoxedInt32Zero;
                case 1: return BoxedInt32One;
                default: return i;
            }
        }
    }
}
