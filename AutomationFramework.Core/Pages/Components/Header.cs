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

    public bool IsVisible() => browser.IsElementVisibleOnPage(repo.HeaderElement);
    public bool IsHeaderWithTextVivible(string headerText) => browser.IsElementVisibleOnPage(repo.HeaderMenyByName(headerText));

    public void ClickOnHomeMenu() => browser.FindElement(repo.HeaderMenyByName("Home")).Click();
    public void ClickOnProductsMenu() => browser.FindElement(repo.HeaderMenyByName("Products")).Click();
    public void ClickOnCartMenu() => browser.FindElement(repo.HeaderMenyByName("Cart")).Click();
    public void ClickOnSignupLoginMenu() => browser.FindElement(repo.HeaderMenyByName("Signup / Login")).Click();
    public void ClickOnTestCasesMenu() => browser.FindElement(repo.HeaderMenyByName("Test Cases")).Click();
    public void ClickOnAPITestingMenu() => browser.FindElement(repo.HeaderMenyByName("API Testing")).Click();
    public void ClickOnVideoTutorialsMenu() => browser.FindElement(repo.HeaderMenyByName("Video Tutorials")).Click();
    public void ClickOnContactUsMenu() => browser.FindElement(repo.HeaderMenyByName("Contact us")).Click();
    public void ClickOnDeleteAccountMenu() => browser.FindElement(repo.HeaderMenyByName("Delete Account")).Click();
}
