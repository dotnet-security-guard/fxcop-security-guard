using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace SecurityGuard
{
    public abstract class BaseRule : BaseIntrospectionRule
    {
        protected BaseRule(string name)
            : base(
                name,
                "SecurityGuard.Rules",
                typeof(BaseRule).Assembly)
        {
        }
    }
}
