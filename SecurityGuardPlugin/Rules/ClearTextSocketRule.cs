using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace SecurityGuard
{
    public class ClearTextSocketRule : BaseRule
    {
        public ClearTextSocketRule() : base("SecurityGuard.ClearTextSocket")
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

            if (callee.BoundMember.FullName.StartsWith("System.Net.Sockets.TcpClient.Connect("))
            {
                this.Problems.Add(new Problem(this.GetResolution(), call));
            }

            base.VisitMethodCall(call);
        }
    }
}
