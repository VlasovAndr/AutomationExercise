using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;

namespace AutomationFramework.Core.Pages;

public class HomePage : PageBase
{
    public Header Header => header;

    protected override string PageName => "Home Page";
    protected override string PageUrl => $"{BaseUrl}";
    
    private readonly Header header;

    public HomePage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header)
        : base(browser, log, config)
    {
        this.header = header;
    }

    public bool IsPageOpened()
    {
        bool isPageOpened = header.IsHeaderBlockVisible() && GetTitle().Equals("Automation Exercise");
        LogPageInfo($"{PageName} {(isPageOpened ? "is opened" : "is not opened")}");

        return isPageOpened;
    }
}
