using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FxCop.Sdk;

namespace SecurityGuard
{
    class MethodAnalysisUtil
    {

        public static bool UsesLiteralString(Expression expression)
        {
            bool result;
            switch (expression.NodeType)
            {
                case NodeType.Literal:
                    result = true;
                    break;
                case NodeType.Local:
                    result = false;
                    break;
                case NodeType.Call:
                    result = false;
                    foreach (Expression operand in ((MethodCall)expression).Operands)
                    {
                        if (UsesLiteralString(operand))
                        {
                            result = true;
                            break;
                        }
                    }
                    break;
                default:
                    result = true;
                    break;
            }

            return result;
        }
    }
}
