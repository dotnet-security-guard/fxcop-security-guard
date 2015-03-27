using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Rules
{
    public class WeakHashingRule : BaseRule
    {
        public WeakHashingRule()
            : base("SecurityGuard.WeakHashing")
        {
            System.Diagnostics.Debugger.Launch();
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

            if (callee.BoundMember.FullName.StartsWith("System.Security.Cryptography.MD5.Create") || callee.BoundMember.FullName.StartsWith("System.Security.Cryptography.SHA1.Create"))
            {
                this.Problems.Add(new Problem(this.GetResolution(), call));
            }

            base.VisitMethodCall(call);
        }       

    }
}
