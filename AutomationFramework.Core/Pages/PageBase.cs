using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;

namespace AutomationFramework.Core.Pages;

public abstract class PageBase
{
    protected readonly IWebDriverWrapper browser;
    protected readonly ILogging log;
    protected string BaseUrl => config.TargetEnvironment.Url;
    protected virtual string PageName { get; }
    protected virtual string PageUrl => BaseUrl;

    private ITestReporter reporter;
    private readonly TestRunConfiguration config;

    public PageBase(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, ITestReporter reporter)
    {
        this.browser = browser;
        this.log = log;
        this.config = config;
        this.reporter = reporter;
    }

    public void Open()
    {
        browser.NavigateToUrl(PageUrl);
        LogPageInfo($"{PageName} is opened");
    }

    public void Close()
    {
        browser.CloseDriver();
        LogPageInfo($"{PageName} is closed");
    }

    protected void LogPageInfo(string logMessage)
    {
        reporter.AddInfo($"|{PageName}| {logMessage}");
    }

    protected void LogParameterInfo(string paramName, string paramValues)
    {
        reporter.AddParameter(paramName, paramValues);
    }
}
