using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Rules
{
    class WeakKeySizeRule : BaseRule
    {

        public WeakKeySizeRule()
            : base("SecurityGuard.WeakKeySize")
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

        public override void VisitConstruct(Construct construct)
        {
            if (construct.Type.FullName.Equals("System.Security.Cryptography.RSACryptoServiceProvider") && (System.Int32)((Literal)construct.Operands[0]).Value < 1024)
            {
                this.Problems.Add(new Problem(this.GetResolution(), construct));
            }
            base.VisitConstruct(construct);
        }


    }
}
