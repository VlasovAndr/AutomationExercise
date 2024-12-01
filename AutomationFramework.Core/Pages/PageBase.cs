using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using Microsoft.Playwright;

namespace AutomationFramework.Core.Pages;

public abstract class PageBase
{
    protected string BaseUrl => config.TargetEnvironment.Url;
    protected virtual string PageName { get; }
    protected virtual string PageUrl => BaseUrl;
    protected IPage Page => page;

    private readonly IPage page;
    private ITestReporter reporter;
    private readonly TestRunConfiguration config;

    public PageBase(IPage page, TestRunConfiguration config, ITestReporter reporter)
    {
        this.page = page;
        this.config = config;
        this.reporter = reporter;
    }

    public async Task Open()
    {
        await Page.GotoAsync(PageUrl);
        LogPageInfo($"{PageName} is opened");
    }

    public async Task ClearPage()
    {
        await Page.Context.ClearCookiesAsync();
        await Page.Context.ClearPermissionsAsync();
        await Page.ReloadAsync();
        LogPageInfo($"{PageName} is cleared");
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
