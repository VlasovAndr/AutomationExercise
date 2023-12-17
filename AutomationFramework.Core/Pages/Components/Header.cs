using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;

namespace AutomationFramework.Core.Pages;

public class Header : PageBase
{
    private readonly HeaderLocators repo;

    public Header(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, HeaderLocators repo)
        : base(browser, log, config)
    {
        this.repo = repo;
    }

    public bool IsHeaderBlockVisible() => browser.IsElementVisibleOnPage(repo.HeaderElement);

    public void GoToHomeMenu() => browser.FindElement(repo.HeaderMenyByName("Home")).Click();
    public void GoToProductsMenu() => browser.FindElement(repo.HeaderMenyByName("Products")).Click();
    public void GoToCartMenu() => browser.FindElement(repo.HeaderMenyByName("Cart")).Click();
    public void GoToSignupLoginMenu() => browser.FindElement(repo.HeaderMenyByName("Signup / Login")).Click();
    public void GoToTestCasesMenu() => browser.FindElement(repo.HeaderMenyByName("Test Cases")).Click();
    public void GoToAPITestingMenu() => browser.FindElement(repo.HeaderMenyByName("API Testing")).Click();
    public void GoToVideoTutorialsMenu() => browser.FindElement(repo.HeaderMenyByName("Video Tutorials")).Click();
    public void GoToContactUsMenu() => browser.FindElement(repo.HeaderMenyByName("Contact us")).Click();
    public void ClickOnDeleteAccountMenu() => browser.FindElement(repo.HeaderMenyByName("Delete Account")).Click();

    public List<string> GetAllHeadersText() => browser.FindElements(repo.HeaderMenyByName(""))
        .Select(x => x.GetAttribute("innerText")).ToList();
}
