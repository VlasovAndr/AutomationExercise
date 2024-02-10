using AutomationFramework.Common.Abstractions;

namespace AutomationFramework.Core.Pages.Components
{
    public abstract class ComponentBase
    {
        private ILogging log;
        private readonly ITestReporter reporter;

        protected virtual string ComponentName { get; }

        public ComponentBase(ILogging log, ITestReporter reporter)
        {
            this.log = log;
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
