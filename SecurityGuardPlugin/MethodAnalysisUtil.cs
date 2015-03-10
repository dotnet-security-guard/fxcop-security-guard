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
            Console.WriteLine(expression.ToString());

            switch (expression.NodeType)
            {
                case NodeType.Literal:
                    return true;
                case NodeType.Call:
                    foreach (Expression operand in ((MethodCall)expression).Operands)
                    {
                        if (!UsesLiteralString(operand))
                        {
                            return false;
                        }
                    }
                    return true;
                default:
                    return false;
            }

        }
    }
}
