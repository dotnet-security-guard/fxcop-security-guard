using Microsoft.FxCop.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard
{
    public class VisitMeRule : BaseRule
    {
        static StreamWriter outfile;

        static Object methodPrint = new Object();

        public VisitMeRule() : base("SecurityGuard.VisitMe")
        {
            //System.Diagnostics.Debugger.Launch();
            if (outfile == null)
            {
                outfile = new StreamWriter(@"C:\Code\fxcop-security-guard\DebugFxCop.txt", true);
                log("VisitMe rule is ready..");
            }
        }

        public override ProblemCollection Check(Member member)
        {
            log(String.Format("Member DeclaringType={0}",member.DeclaringType.FullName));
            Method meth = member as Method;
            if(meth != null) {
                lock (methodPrint)
                {
                    this.Visit(meth);
                }
            }
            return Problems;
        }

        public override ProblemCollection Check(Resource resource) {
            string data = Encoding.UTF8.GetString(resource.Data);
            log(String.Format("Resource name={0} data={1}[..]", //
                resource.Name, //
               data.Substring(0,Math.Min(20,data.Length))));
            return Problems;
        }


        public override void Visit(Node node)
        {
            if(node.NodeType == NodeType.Method)
            {
                log("====");
            }
            else if (node.NodeType == NodeType.MemberBinding)
            {
                MemberBinding callee = (MemberBinding) node;
                log(String.Format("({1}:{2}) Method Call: {0}", 
                    callee.BoundMember.FullName,
                    Path.GetFileName(node.SourceContext.FileName),
                    node.SourceContext.StartLine));
            }
            else
            {
                log(String.Format("({2}:{3}) Node : type={0} val={1}",
                    node.NodeType.ToString(),
                    node.ToString(),
                    Path.GetFileName(node.SourceContext.FileName),
                    node.SourceContext.StartLine));
            }
            
            base.Visit(node);
        }

        private void log(string msg) {
            lock (outfile)
            {
                outfile.WriteLine(msg);
                outfile.Flush();
            }
        }

    }
}
