using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace SecurityGuard.Rules
{
    class ClearTextHttpRule : BaseRule
    {
        public ClearTextHttpRule()
            : base("SecurityGuard.ClearTextHttp")
        {
            //System.Diagnostics.Debugger.Launch();
        }


        public override ProblemCollection Check(Member member)
        {
            Method meth = member as Method;
            if (meth != null)
            {
                this.Visit(meth);
            }

            return Problems;
        }

        public override void VisitMethodCall(MethodCall call)
        {
            MemberBinding callee = (MemberBinding) call.Callee;

            if (callee.BoundMember.FullName.StartsWith("System.Net.WebRequest.Create(System.String"))
            {
                validateOperand(call.Operands[0]);
            }

            base.VisitMethodCall(call);
        }

        public override void VisitConstruct(Construct construct)
        {

            Method method = (Method)((MemberBinding)construct.Constructor).BoundMember;
            if (method.DeclaringType.FullName.StartsWith("System.Uri"))
            {
                validateOperand(construct.Operands[0]);
            }

            base.VisitConstruct(construct);
        }

        private void validateOperand(Expression expression) {
            String uri = MethodAnalysisUtil.GetStringValue(expression);
            if (uri != null && uri.StartsWith("http://"))
            {
                this.Problems.Add(new Problem(this.GetResolution(), expression));
            }
        }
    }
}
