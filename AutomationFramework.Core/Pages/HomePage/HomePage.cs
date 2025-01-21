using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Common.Header;
using NUnit.Allure.Attributes;

namespace AutomationFramework.Core.Pages.HomePage;

public class HomePage : PageBase
{
    public Header Header { get; }

    protected override string PageName => pageName;

    private const string pageName = "Home Page";

    public HomePage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header, ITestReporter reporter)
        : base(browser, log, config, reporter)
    {
        Header = header;
    }

    [AllureStep($"|{pageName}| Getting page status")]
    public bool IsPageOpened()
    {
        var isPageOpened = Header.IsHeaderBlockVisible() && browser.WebDriver.Title.Equals("Automation Exercise");
        LogParameterInfo("isPageOpened", isPageOpened.ToString());

        return isPageOpened;
    }
}
