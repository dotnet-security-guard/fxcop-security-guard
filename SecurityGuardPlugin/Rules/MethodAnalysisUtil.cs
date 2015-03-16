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
        //Inspired by : https://social.msdn.microsoft.com/Forums/en-US/c6711b6e-60e0-4cf7-bc76-7ab0989d5b12/fxcop-136-custom-rule-question?forum=vstscode
        public static bool UsesLiteralString(Expression expression)
        {
            switch (expression.NodeType)
            {
                case NodeType.Literal:
                    return true;
                /*case NodeType.Call:
                    foreach (Expression operand in ((MethodCall)expression).Operands)
                    {
                        if (!UsesLiteralString(operand))
                        {
                            return false;
                        }
                    }
                    return true;*/
                default:
                    return false;
            }

        }

        public static string GetStringValue(Expression expression)
        {
            if (expression.NodeType == NodeType.Literal) {
                return expression.ToString();
            }
            else {
                return null;
            }
        }
    }
}
