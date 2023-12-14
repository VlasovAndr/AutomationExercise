using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;

namespace AutomationFramework.Core.Pages;

public class HomePage : PageBase
{
    private const string PAGE_NAME = "Home Page";
    public readonly Header header;

    public HomePage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header)
        : base(browser, log, config)
    {
        this.header = header;
    }

    public void Open()
    {
        browser.NavigateToUrl(BaseUrl);
        log.Information($"|{PAGE_NAME}| {PAGE_NAME} is opened");
    }

    public bool IsPageOpen()
    {
        return header.IsVisible() && GetTitle().Equals("Automation Exercise");
    }
}
