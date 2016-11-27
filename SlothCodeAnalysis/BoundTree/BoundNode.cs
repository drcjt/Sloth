using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.BoundTree
{
    public abstract class BoundNode
    {
        private readonly BoundKind _kind;

        public readonly SyntaxNode Syntax;

        protected BoundNode(BoundKind kind, SyntaxNode syntax)
        {
            _kind = kind;
            Syntax = syntax;
        }

        public virtual BoundNode Accept(BoundTreeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public BoundKind Kind
        {
            get
            {
                return _kind;
            }
        }
    }
}
