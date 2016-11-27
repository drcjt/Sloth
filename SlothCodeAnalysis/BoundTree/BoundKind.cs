using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.BoundTree
{
    public enum BoundKind : byte
    {
        BadExpression,
        BadStatement,
        BinaryOperator,
        AssignmentOperator,
        IfStatement,
        WhileStatement,
        ForStatement,
        StatementList,
        Block,
        Literal,
    }
}
