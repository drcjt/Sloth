using SlothCodeAnalysis.BoundTree;
using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace SlothCodeAnalysis.Binder
{
    public partial class Binder
    {
        internal bool CheckValueKind(BoundExpression expr, BindValueKind valueKind)
        {
            /*
            if (expr.HasAnyErrors)
            {
                return false;
            }
            */

            switch (expr.Kind)
            {
                default:
                    {
                        if (RequiresSettingValue(valueKind))
                        {
                            if (!CheckIsVariable(expr.Syntax, expr, valueKind, false))
                            {
                                return false;
                            }
                        }

                        /*
                        if (RequiresGettingValue(valueKind))
                        {
                        }
                        */

                        return true;
                    }
            }
        }

        /// <summary>
        /// The purpose of this method is to determine if the expression is classified by the 
        /// specification as a *variable*. If it is not then this code gives an appropriate error message.
        ///
        /// To determine the appropriate error message we need to know two things:
        ///
        /// (1) why do we want to know if this is a variable? Because we are trying to assign it,
        ///     increment it, or pass it by reference?
        ///
        /// (2) Are we trying to determine if the left hand side of a dot is a variable in order
        ///     to determine if the field or property on the right hand side of a dot is assignable?
        /// </summary>
        private bool CheckIsVariable(SyntaxNode node, BoundExpression expr, BindValueKind kind, bool checkingReceiver)
        {
            Debug.Assert(expr != null);
            //Debug.Assert(!checkingReceiver || expr.Type.IsValueType || expr.Type.IsTypeParameter());

            // Every expression is classified as one of:
            // 4. a literal
            // 10. a variable
            // 11. a value

            // We wish to give an error and return false for all of those except case 10.

            // case 0: We've already reported an error:

            return false;
        }
    }
}
