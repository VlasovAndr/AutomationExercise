using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Pages.Components;
using AutomationFramework.Core.Pages.Locators;
using Microsoft.Playwright;
using NUnit.Allure.Attributes;

namespace AutomationFramework.Core.Pages;

public class Header : ComponentBase
{
    private readonly HeaderLocators repo;
    private const string componentName = "Header";

    protected override string ComponentName => componentName;

    public Header(IPage page, HeaderLocators repo, ITestReporter reporter)
        : base(page, reporter)
    {
        this.repo = repo;
    }

    [AllureStep($"|{componentName}| Getting header block visibility status")]
    public async Task<bool> IsHeaderBlockVisible()
    {
        var isHeaderBlockVisible = await Page.Locator(repo.HeaderElement).IsVisibleAsync();
        LogParameterInfo("isHeaderBlockVisible", isHeaderBlockVisible.ToString());
        return isHeaderBlockVisible;
    }

    public async Task GoToHomeMenu() => await GoToMenu("Home");
    public async Task GoToProductsMenu() => await GoToMenu("Products");
    public async Task GoToCartMenu() => await GoToMenu("Cart");
    public async Task GoToSignupLoginMenu() => await GoToMenu("Signup / Login");
    public async Task GoToTestCasesMenu() => await GoToMenu("Test Cases");
    public async Task GoToAPITestingMenu() => await GoToMenu("API Testing");
    public async Task GoToVideoTutorialsMenu() => await GoToMenu("Video Tutorials");
    public async Task GoToContactUsMenu() => await GoToMenu("Contact us");
    public async Task ClickOnDeleteAccountMenu() => await GoToMenu("Delete Account");
    public async Task ClickOnLogoutMenu() => await GoToMenu("Logout");

    public async Task<IReadOnlyList<string>> GetAllHeadersText()
    {
        return await Page.Locator(repo.HeaderMenuByName("")).AllInnerTextsAsync();
    } 

    [AllureStep($"|{componentName}| Go to {{menuName}} menu")]
    private async Task GoToMenu(string menuName)
    {
        await Page.Locator(repo.HeaderMenuByName(menuName)).ClickAsync();
    }
}
