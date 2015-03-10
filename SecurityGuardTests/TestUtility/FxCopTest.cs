using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecurityGuard.Tests
{
    public class FxCopTest
    {
        private const string FXCOP_HOME = "C:/Program Files (x86)/Microsoft Visual Studio 12.0/Team Tools/Static Analysis Tools/FxCop/";
        private const string SEC_GUARD_HOME = "C:/Code/fxcop-security-guard/";
        private const bool DISPLAY_FXCOP_OUT = false;

        private Task[] taskWait = { };

        private static ResultsVerifier resultsVerifier;


        private String getDllPath(String projectName) {
            return String.Format("{0}/bin/Debug/{0}.dll", projectName);
        }

        public List<FxCopIssue> runScan() {

            string targetBinary = SEC_GUARD_HOME + getDllPath("SecurityGuardSamples");
            string ruleFile = SEC_GUARD_HOME + getDllPath("SecurityGuardPlugin");
            string tempReportFile = System.IO.Path.GetTempPath() + "find-sec-guard" + Guid.NewGuid().ToString() + ".xml";


            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = FXCOP_HOME + "FxCopCmd.exe",
                    Arguments = String.Format("/f:{0} /r:{1} /out:{2}", targetBinary, ruleFile, tempReportFile),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Normal
                }
            };
            Console.WriteLine("Executing : " + "FxCopCmd.exe " + process.StartInfo.Arguments);
            process.Start();

            if (DISPLAY_FXCOP_OUT)
            {
                displayOut(process);
                Console.WriteLine("waiting for " + taskWait.Length);
                Task.WaitAll(taskWait, 15 * 1000);
            }

            process.WaitForExit();

            if (!File.Exists(tempReportFile))
            {
                Assert.Fail("No report file found.");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(tempReportFile);

            //Loading the issue from the XML report

            var issues = new List<FxCopIssue>();
            XmlNodeList allMsgNodes = doc.DocumentElement.SelectNodes("//Message");
            foreach (XmlNode msgNode in allMsgNodes)
            {
                //Console.WriteLine(msgNode.InnerXml);
                foreach(XmlNode issueNode in msgNode.ChildNodes) {
                    issues.Add(new FxCopIssue() { 
                        description = issueNode.InnerXml,
                        file = issueNode.Attributes["File"].Value,
                        line = Int32.Parse(issueNode.Attributes["Line"].Value),
                        issueType = msgNode.Attributes["TypeName"].Value
                    });
                }
                
            }
            return issues;
        }


        private void displayOut(Process process)
        {


            var outputTask = new Task(() =>
            {
                try
                {
                    while (!process.StandardOutput.EndOfStream)
                        Console.WriteLine("> " + process.StandardOutput.ReadLine());
                }
                catch (InvalidOperationException e)
                {
                }
            });
            outputTask.Start();

            var errorTask = new Task(() =>
            {
                try
                {
                    while (!process.StandardError.EndOfStream)
                        Console.WriteLine("! " + process.StandardError.ReadLine());
                }
                catch (InvalidOperationException e)
                {
                }
            });
            errorTask.Start();

            taskWait = new Task[] { outputTask, errorTask };
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public ResultsVerifier getVerifier() {
            if(resultsVerifier == null) {
                Console.WriteLine("Running scan ..");
                List<FxCopIssue> allIssues = runScan();
                resultsVerifier = new ResultsVerifier(allIssues);
            }
            return resultsVerifier; 
        }
    }
}
