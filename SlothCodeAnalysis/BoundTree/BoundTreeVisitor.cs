using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.BoundTree
{
    public class BoundTreeVisitor
    {
        protected BoundTreeVisitor()
        {
        }

        public virtual BoundNode Visit(BoundNode node)
        {
            if (node != null)
            {
                return node.Accept(this);
            }

            return null;
        }

        public virtual BoundNode DefaultVisit(BoundNode node)
        {
            return null;
        }

        public virtual BoundNode VisitIfStatement(BoundIfStatement node)
        {
            return this.DefaultVisit(node);
        }

        public virtual BoundNode VisitForStatement(BoundForStatement node)
        {
            return this.DefaultVisit(node);
        }

        public virtual BoundNode VisitStatementList(BoundStatementList node)
        {
            return this.DefaultVisit(node);
        }

        public virtual BoundNode VisitBlock(BoundBlock node)
        {
            return this.DefaultVisit(node);
        }

        public virtual BoundNode VisitLiteral(BoundLiteral node)
        {
            return this.DefaultVisit(node);
        }

        public virtual BoundNode VisitBinaryOperator(BoundBinaryOperator node)
        {
            return this.DefaultVisit(node);
        }
    }
}
