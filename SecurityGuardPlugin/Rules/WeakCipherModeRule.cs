using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Rules
{
    class WeakCipherModeRule : BaseRule
    {

        public WeakCipherModeRule()
            : base("SecurityGuard.WeakCipherMode")
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

            // namespace System.Security.Cryptography, CipherMode{CBC = 1,ECB = 2,OFB = 3,CFB = 4,CTS = 5}
            if (callee.BoundMember.FullName.StartsWith("System.Security.Cryptography.SymmetricAlgorithm.set_Mode") && (System.Int32)((Literal)call.Operands[0]).Value == 2)  
            {
                this.Problems.Add(new Problem(this.GetResolution(), call));
            }

            base.VisitMethodCall(call);
        }

    }
}
