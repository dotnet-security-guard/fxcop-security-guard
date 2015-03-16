using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard
{
    public class WeakCertificateValidationRule : BaseRule
    {

        public WeakCertificateValidationRule() : base("SecurityGuard.WeakCertificateValidation")
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
            MemberBinding callee = (MemberBinding)call.Callee;

            if (callee.BoundMember.FullName.StartsWith("System.Net.ServicePointManager.set_ServerCertificateValidationCallback(") ||
                callee.BoundMember.FullName.StartsWith("System.Net.ServicePointManager.set_CertificatePolicy(System.Net.ICertificatePolicy"))
            {
                this.Problems.Add(new Problem(this.GetResolution(), call));
            }

            base.VisitMethodCall(call);
        }
    }
}
