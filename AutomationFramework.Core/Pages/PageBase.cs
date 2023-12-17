using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;

namespace AutomationFramework.Core.Pages;

public class PageBase
{
    protected readonly IWebDriverWrapper browser;
    protected readonly ILogging log;
    private readonly TestRunConfiguration config;

    protected string BaseUrl => config.TargetEnvironment.Url;

    public PageBase(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config)
    {
        this.browser = browser;
        this.log = log;
        this.config = config;
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
}
