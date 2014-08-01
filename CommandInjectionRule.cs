using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace SecurityGuard
{
    public class CommandInjectionRule : BaseRule
    {
        public CommandInjectionRule() : base("CommandInjection")
        {
        }

        public override ProblemCollection Check(Member member)
        {
            Method meth = member as Method;
            if (meth != null) {
                this.Visit(meth);
            }

            return Problems;
        }

        public override void VisitMethodCall(MethodCall call)
        {
            MemberBinding callee = (MemberBinding)call.Callee;

            //Process.Start
            //List of signatures to cover : http://msdn.microsoft.com/en-us/library/vstudio/system.diagnostics.process.start
            if (callee.BoundMember.FullName.StartsWith("System.Diagnostics.Process.Start("))
            {
                if (MethodAnalysisUtil.UsesLiteralString(call.Operands[0]))
                {
                    this.Problems.Add(new Problem(this.GetResolution(), call));
                }
            }

            base.VisitMethodCall(call);
        }



    }

}
