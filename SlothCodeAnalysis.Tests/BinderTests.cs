﻿using NUnit.Framework;
using SlothCodeAnalysis.Syntax;
using SlothCodeAnalysis.Binder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothCodeAnalysis.Compilation;

namespace SlothCodeAnalysis.Tests
{
    [TestFixture]
    class BinderTests
    {
        [Test]
        public void TestParsePrintStatement()
        {
            var text = @"print 3 + 4 * ""foo"";";
            var tree = SyntaxTree.ParseText(text);
            var compilation = new SlothCompilation(tree);
            var model = compilation.GetSemanticModel(tree);

            var root = tree.GetRoot() as CompilationUnitSyntax;
            var printStmt = root.Statements[0] as PrintStatementSyntax;

            var binder = new Binder.Binder(compilation);

            var diagnostics = new BindingDiagnosticBag();

            var boundExpression = SyntaxTreeSemanticModel.GetBoundExpressionHelper(binder, printStmt.Expression, diagnostics);

            Assert.AreEqual(2, diagnostics.DiagnosticBag.AsCollection().Count);
        }
    }
}
