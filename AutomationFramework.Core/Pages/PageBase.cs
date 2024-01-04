using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;

namespace AutomationFramework.Core.Pages;

public abstract class PageBase
{
    protected readonly IWebDriverWrapper browser;
    protected readonly ILogging log;
    protected string BaseUrl => config.TargetEnvironment.Url;
    protected virtual string PageName { get; }
    protected virtual string PageUrl { get; }

    private readonly TestRunConfiguration config;

    public PageBase(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config)
    {
        this.browser = browser;
        this.log = log;
        this.config = config;
    }

    public void Open()
    {
        browser.NavigateToUrl(PageUrl);
        LogPageInfo($"{PageName} is opened");
    }

    public string GetTitle()
    {
        var title = browser.WebDriver.Title;
        log.Information($"Current title - {title}");

        return title;
    }

    public void NavigateToUrl(string url)
    {
        browser.NavigateToUrl(url);
        log.Information($"Navigating to - {url}");
    }

    public void GoToTab(int tabIndex)
    {
        browser.GoToTab(tabIndex);
        log.Information($"Going to tab by index '{tabIndex}'");
    }

    public void CloseCurrentTab()
    {
        browser.CloseCurrentTab();
        log.Information($"Current tab is closed");
    }

    public void Close()
    {
        browser.CloseDriver();
        log.Information($"Browser is closed");
    }

    protected void LogPageInfo(string logMessage)
    {
        log.Information($"|{PageName}| {logMessage}");
    }
}
