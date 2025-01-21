using AutomationFramework.Common.Abstractions;

namespace AutomationFramework.Core.Pages.Components
{
    public abstract class ComponentBase
    {
        private ILogging log;
        private readonly ITestReporter reporter;
        protected readonly IWebDriverWrapper browser;

        protected virtual string ComponentName { get; }

        public ComponentBase(IWebDriverWrapper browser, ILogging log, ITestReporter reporter)
        {
            this.browser = browser;
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

        protected void CreateStep(string log, Action action)
        {
            reporter.CreateStep($"|{ComponentName}| {log}", action);
        }
    }
}
