using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace SecurityGuard.Rules
{
    class XPathInjectionRule : BaseRule
    {
        public XPathInjectionRule() : base("SecurityGuard.XPathInjection")
        {
            //System.Diagnostics.Debugger.Launch();
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
            MemberBinding callee = (MemberBinding) call.Callee;

            if (callee.BoundMember.FullName.StartsWith("System.Xml.XmlNode.SelectNodes(System.String)") ||
                callee.BoundMember.FullName.StartsWith("System.Xml.XmlNode.SelectSingleNode(System.String)"))
            {
                if (!MethodAnalysisUtil.UsesLiteralString(call.Operands[0]))
                {
                    this.Problems.Add(new Problem(this.GetResolution(), call));
                }
            }
            
            base.VisitMethodCall(call);
        }
    }
}
