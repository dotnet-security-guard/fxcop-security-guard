using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Rules
{
    public class WeakCipherAlgorithmRule : BaseRule
    {

        public WeakCipherAlgorithmRule()
            : base("SecurityGuard.WeakCipherAlgorithm")
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

            if (callee.BoundMember.FullName.StartsWith("System.Security.Cryptography.DES.Create"))
            {
                this.Problems.Add(new Problem(this.GetResolution(), call));
            }

            base.VisitMethodCall(call);
        }

        public override void VisitConstruct(Construct construct)
        {
            if (construct.Type.FullName.Equals("System.Security.Cryptography.DESCryptoServiceProvider"))
            {
                this.Problems.Add(new Problem(this.GetResolution(), construct));
            }
            base.VisitConstruct(construct);
        }

    }
}
