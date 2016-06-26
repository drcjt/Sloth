using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SlothCodeAnalysis.Syntax;

namespace SlothCodeAnalysis.Tests
{
    [TestFixture]
    class ParserTests
    {
        [Test]
        public void TestParsePrintStatement()
        {
            var text = "print 3 + 4 * 5;";
            SyntaxFactory.ParseCompilationUnit(text);
        }
    }
}
