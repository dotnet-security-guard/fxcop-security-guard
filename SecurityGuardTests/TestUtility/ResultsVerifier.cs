using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityGuard.Tests
{
    public class ResultsVerifier
    {
        private List<FxCopIssue> issues = null;

        public ResultsVerifier(List<FxCopIssue> issues)
        {
            this.issues = issues;
        }

        public void assertIssueFound(String type, String file, int line)
        {

            foreach(FxCopIssue issue in issues) {
                if (issue.issueType.Equals(type) &&
                    issue.file.Equals(file) &&
                    issue.line.Equals(line)) {
                        Console.WriteLine("Successful assertion : {0} in {1} (line:{2})", type, file, line);
                        return;
                }
                else if(issue.file.Equals(file)) { //For easier troubleshooting all the issues are list.
                    Console.WriteLine("Issue : <{0}> matching the file {1} (line:{2})", issue.ToString(), file, line);
                }
            }

            Assert.Fail(String.Format("The issue {0} was not found in {1} (line:{2}). See test 'Output' for more information.",type,file,line));
        }

        public void assertIssueCount(int expectedCount, String type, String file)
        {
            int actualCount = 0;
            foreach (FxCopIssue issue in issues)
            {
                if (issue.issueType.Equals(type) &&
                    issue.file.Equals(file))
                {
                    actualCount++;
                }
            }

            Assert.AreEqual(expectedCount, actualCount, String.Format("Wrong number of issues for the type '{0}'", type));
        }
    }
}
