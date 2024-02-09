using OpenQA.Selenium;

namespace AutomationFramework.Common.Abstractions;

public interface IWebDriverWrapper
{
    IWebDriver WebDriver { get; }

    bool IsWebDriverCreated { get; }

    void NavigateToUrl(string url);

    void CloseDriver();

    IWebElement FindElement(string XPath, int timeInSeconds = 10);

    void EnterText(string xPath, string text);

    IWebElement FindElement(string XPath, string frameName, int timeInSeconds = 10);

    List<IWebElement> FindElements(string XPath, int timeInSeconds = 10);

    void MoveToElement(string XPath);

    void DoubleClickToElement(string XPath);

    void RightClickToElement(string XPath);

    object ExecuteAsyncJSScriptForElement(string script, IWebElement element);

    object ExecuteAsyncJSScript(string script);

    object ExecuteJSScriptWithParam(string script, params object[] args);

    bool IsElementVisibleOnPage(string XPath, int timeInSeconds = 10);

    IWebElement GetElementFromDOM(string XPath, int timeInSeconds = 10);

    void WaitSomeSeconds(int timeInSeconds = 5);

    void GoToTab(int tabIndex);

    void CloseCurrentTab();

    void SaveScreenshot(string path);
    
    byte[] GetScreenshot();

    public string GetBrowserLogs();

    void SelectFromDropDownByText(string xPath, string text);

    void SelectFromDropDownByValue(string xPath, string value);

    void SelectFromDropDownByIndex(string xPath, int index);
}
