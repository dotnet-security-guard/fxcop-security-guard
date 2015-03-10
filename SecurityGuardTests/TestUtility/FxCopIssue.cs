using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityGuard.Tests
{
    public class FxCopIssue
    {
        public string issueType {get;set;}
        public string file { get; set; }
        public int line { get; set; }
        public string description { get; set; }


        public string toString() {
            return issueType+" ("+description.Substring(Math.Min(description.Length,15))+"[..])";
        }
    }
}
