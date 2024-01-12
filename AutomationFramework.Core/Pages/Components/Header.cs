using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Pages.Locators;

namespace AutomationFramework.Core.Pages;

public class Header
{
    private readonly HeaderLocators repo;
    private readonly IWebDriverWrapper browser;
    private readonly ILogging log;

    public Header(IWebDriverWrapper browser, ILogging log, HeaderLocators repo)
    {
        this.repo = repo;
        this.browser = browser;
        this.log = log;
    }

    public bool IsHeaderBlockVisible() => browser.IsElementVisibleOnPage(repo.HeaderElement);

    public void GoToHomeMenu() => browser.FindElement(repo.HeaderMenuByName("Home")).Click();
    public void GoToProductsMenu() => browser.FindElement(repo.HeaderMenuByName("Products")).Click();
    public void GoToCartMenu() => browser.FindElement(repo.HeaderMenuByName("Cart")).Click();
    public void GoToSignupLoginMenu() => browser.FindElement(repo.HeaderMenuByName("Signup / Login")).Click();
    public void GoToTestCasesMenu() => browser.FindElement(repo.HeaderMenuByName("Test Cases")).Click();
    public void GoToAPITestingMenu() => browser.FindElement(repo.HeaderMenuByName("API Testing")).Click();
    public void GoToVideoTutorialsMenu() => browser.FindElement(repo.HeaderMenuByName("Video Tutorials")).Click();
    public void GoToContactUsMenu() => browser.FindElement(repo.HeaderMenuByName("Contact us")).Click();
    public void ClickOnDeleteAccountMenu() => browser.FindElement(repo.HeaderMenuByName("Delete Account")).Click();
    public void ClickOnLogoutMenu() => browser.FindElement(repo.HeaderMenuByName("Logout")).Click();

    public List<string> GetAllHeadersText() => browser.FindElements(repo.HeaderMenuByName(""))
        .Select(x => x.GetAttribute("innerText")).ToList();
}
