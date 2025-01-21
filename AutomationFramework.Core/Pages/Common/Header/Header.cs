using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Pages.Components;
using NUnit.Allure.Attributes;

namespace AutomationFramework.Core.Pages.Common.Header;

public class Header : ComponentBase
{
    private readonly HeaderLocators repo;
    private const string componentName = "Header";

    protected override string ComponentName => componentName;

    public Header(IWebDriverWrapper browser, ILogging log, HeaderLocators repo, ITestReporter reporter)
        : base(browser, log, reporter)
    {
        this.repo = repo;
    }

    [AllureStep($"|{componentName}| Getting header block visibility status")]
    public bool IsHeaderBlockVisible()
    {
        var isHeaderBlockVisible = browser.IsElementVisibleOnPage(repo.HeaderElement);
        LogParameterInfo("isHeaderBlockVisible", isHeaderBlockVisible.ToString());
        return isHeaderBlockVisible;
    }

    public void GoToHomeMenu() => GoToMenu("Home");
    public void GoToProductsMenu() => GoToMenu("Products");
    public void GoToCartMenu() => GoToMenu("Cart");
    public void GoToSignupLoginMenu() => GoToMenu("Signup / Login");
    public void GoToTestCasesMenu() => GoToMenu("Test Cases");
    public void GoToAPITestingMenu() => GoToMenu("API Testing");
    public void GoToVideoTutorialsMenu() => GoToMenu("Video Tutorials");
    public void GoToContactUsMenu() => GoToMenu("Contact us");
    public void ClickOnDeleteAccountMenu() => GoToMenu("Delete Account");
    public void ClickOnLogoutMenu() => GoToMenu("Logout");

    public List<string> GetAllHeadersText() => browser.FindElements(repo.HeaderMenuByName(""))
        .Select(x => x.GetAttribute("innerText")).ToList();

    [AllureStep($"|{componentName}| Go to {{menuName}} menu")]
    private void GoToMenu(string menuName)
    {
        browser.FindElement(repo.HeaderMenuByName(menuName)).Click();
    }
}
