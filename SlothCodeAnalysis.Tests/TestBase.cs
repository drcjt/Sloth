using SlothCodeAnalysis.Compilation;
using SlothCodeAnalysis.Syntax;
using SlothCodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlothCodeAnalysis.Tests
{
    class TestBase
    {
        private static SlothCompilation CreateCompilation(TestSource source)
        {
            var syntaxTree = source.GetSyntaxTree();

            SlothCompilation compilation = SlothCompilation.Create(syntaxTree);

            return compilation;
        }

        public static SyntaxTree Parse(string text)
        {
            var stringText = StringText.From(text);
            return SyntaxFactory.ParseSyntaxTree(stringText);
        }
    }
}
