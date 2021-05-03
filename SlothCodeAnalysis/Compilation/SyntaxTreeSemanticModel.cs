using SlothCodeAnalysis.Binder;
using SlothCodeAnalysis.BoundTree;
using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Compilation
{
    public class SyntaxTreeSemanticModel
    {
        private readonly SlothCompilation _compilation;
        private readonly SyntaxTree _syntaxTree;

        internal SyntaxTreeSemanticModel(SlothCompilation compilation, SyntaxTree syntaxTree)
        {
            _compilation = compilation;
            _syntaxTree = syntaxTree;

            // Binder stuff goes here!!
        }

        /// <summary>
        /// The compilation this object was obtained from.
        /// </summary>
        public SlothCompilation Compilation
        {
            get
            {
                return _compilation;
            }
        }

        /// <summary>
        /// The root node of the syntax tree that this object is associated with.
        /// </summary>
        internal SyntaxNode Root
        {
            get
            {
                return (SyntaxNode)_syntaxTree.GetRoot();
            }
        }

        /// <summary>
        /// The SyntaxTree that this object is associated with.
        /// </summary>
        public SyntaxTree SyntaxTree
        {
            get
            {
                return _syntaxTree;
            }
        }

        public static BoundExpression GetBoundExpressionHelper(SlothCodeAnalysis.Binder.Binder binder, ExpressionSyntax expression, BindingDiagnosticBag diagnostics)
        {
            Debug.Assert(binder != null);
            Debug.Assert(expression != null);

            BoundExpression boundNode;
            boundNode = binder.BindExpression(expression, diagnostics);

            return boundNode;
        }
    }
}
