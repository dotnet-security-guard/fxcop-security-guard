using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace SecurityGuard.Tests
{
    public class FxCopTest
    {
        private string[] FXCOP_HOME = {"C:/Program Files/Microsoft Visual Studio 12.0/Team Tools/Static Analysis Tools/FxCop/",
                                      "C:/Program Files (x86)/Microsoft Visual Studio 12.0/Team Tools/Static Analysis Tools/FxCop/"};
        private const bool DISPLAY_FXCOP_OUT = false;

        private Task[] taskWait = { };

        private static ResultsVerifier resultsVerifier;


        private String getDllPath(String projectName) {
            return String.Format("{0}/bin/Debug/{0}.dll", projectName);
        }

        public List<FxCopIssue> runScan() {

            //Guessing some directory
            string fxCopHome = findHomeDirectory();
            string secGuardHome = findSecGuardHomeDirectory();


            string targetBinary = secGuardHome + getDllPath("SecurityGuardSamples");
            string ruleFile = secGuardHome + getDllPath("SecurityGuardPlugin");
            string tempReportFile = System.IO.Path.GetTempPath() + "find-sec-guard" + Guid.NewGuid().ToString() + ".xml";

            Console.WriteLine("== Configuration ==");
            Console.WriteLine("FxCop home directory \t: " + fxCopHome);
            Console.WriteLine("Security Guard directory \t: " + secGuardHome);
            Console.WriteLine("Target DLL (Samples) \t: " + targetBinary);
            Console.WriteLine("Plugin DLL \t: " + ruleFile);
            Console.WriteLine("Report output file \t: " + tempReportFile);
            Console.WriteLine("=============");

            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fxCopHome + "FxCopCmd.exe",
                    Arguments = String.Format("/f:\"{0}\" /r:\"{1}\" /out:\"{2}\"", targetBinary, ruleFile, tempReportFile),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Normal
                }
            };
            Console.WriteLine("Executing : FxCopCmd.exe " + process.StartInfo.Arguments);
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
            XmlNodeList allExceptionNodes = doc.DocumentElement.SelectNodes("//Exception");
            foreach(XmlNode exceptionNode in allExceptionNodes) {
                String exType = exceptionNode.SelectSingleNode("Type").InnerText;
                String exMsg = exceptionNode.SelectSingleNode("ExceptionMessage").InnerText;
                Console.WriteLine(" [!] "+exType+": "+exMsg);
            }
            
            return issues;
        }


        private string findHomeDirectory()
        {
            foreach(string path in FXCOP_HOME) {
                if (File.Exists(path + "FxCopCmd.exe")) {
                    return path;
                }
            }

            throw new System.Exception("Unable to find the FxCop home directory");
        }


        private string findSecGuardHomeDirectory()
        {
            MatchCollection matches = new Regex("(.*)SecurityGuardTests").Matches(Directory.GetCurrentDirectory());
            if (matches.Count > 0)
            {
                return matches[0].Groups[1].Value;
            }

            throw new System.Exception("Unable to find the root of the project.");
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
