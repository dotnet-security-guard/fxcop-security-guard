using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityGuard
{
    public class WeakRandomGeneratorRule : BaseRule
    {
        public WeakRandomGeneratorRule()
            : base("SecurityGuard.WeakRandomGenerator")
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

        public override void VisitConstruct(Construct construct) {
            if (construct.Type.FullName.Equals("System.Random")) {
                this.Problems.Add(new Problem(this.GetResolution(), construct));
            }
            base.VisitConstruct(construct);
        }
    }
}
