using AutomationFramework.Common.Abstractions;
using Microsoft.Playwright;

namespace AutomationFramework.Core.Pages.Components
{
    public abstract class ComponentBase
    {
        private readonly ITestReporter reporter;
        private readonly IPage page;
        protected IPage Page => page;

        protected virtual string ComponentName { get; }

        public ComponentBase(IPage page, ITestReporter reporter)
        {
            this.page = page;
            this.reporter = reporter;
        }

        protected void LogComponentInfo(string logMessage)
        {
            reporter.AddInfo($"|{ComponentName}| {logMessage}");
        }

        protected void LogParameterInfo(string paramName, string paramValues)
        {
            reporter.AddParameter(paramName, paramValues);
        }
    }
}
