using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using Microsoft.Playwright;
using NUnit.Allure.Attributes;

namespace AutomationFramework.Core.Pages;

public class HomePage : PageBase
{
    public Header Header => header;

    protected override string PageName => pageName;

    private const string pageName = "Home Page";
    private readonly Header header;

    public HomePage(IPage page, TestRunConfiguration config, Header header, ITestReporter reporter)
        : base(page, config, reporter)
    {
        this.header = header;
    }

    [AllureStep($"|{pageName}| Getting page status")]
    public async Task<bool> IsPageOpened()
    {
        var isPageOpened = await header.IsHeaderBlockVisible() && Page.TitleAsync().Result.Equals("Automation Exercise");
        LogParameterInfo("isPageOpened", isPageOpened.ToString());

        return isPageOpened;
    }
}
