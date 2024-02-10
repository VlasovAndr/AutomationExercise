using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using NUnit.Allure.Attributes;

namespace AutomationFramework.Core.Pages;

public class HomePage : PageBase
{
    public Header Header => header;

    protected override string PageName => pageName;

    private const string pageName = "Home Page";
    private readonly Header header;

    public HomePage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header, ITestReporter reporter)
        : base(browser, log, config, reporter)
    {
        this.header = header;
    }

    [AllureStep($"|{pageName}| Getting page status")]
    public bool IsPageOpened()
    {
        var isPageOpened = header.IsHeaderBlockVisible() && browser.WebDriver.Title.Equals("Automation Exercise");
        LogParameterInfo("isPageOpened", isPageOpened.ToString());

        return isPageOpened;
    }
}
