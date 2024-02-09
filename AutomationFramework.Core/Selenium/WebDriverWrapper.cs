using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Core.Selenium;

public class WebDriverWrapper : IWebDriverWrapper
{
    public IWebDriver WebDriver => webDriverService.Value;
    public bool IsWebDriverCreated => webDriverService.IsValueCreated;
    private Lazy<IWebDriver> webDriverService;
    private readonly IServiceProvider serviceProvider;
    private readonly TestRunConfiguration config;
    private ILogging log;

    public WebDriverWrapper(IServiceProvider serviceProvider, ILogging log, TestRunConfiguration config)
    {
        this.serviceProvider = serviceProvider;
        this.config = config;
        this.log = log;
        webDriverService = new Lazy<IWebDriver>(CreateWebDriver, true);
    }

    private IWebDriver CreateWebDriver()
    {
        var factory = serviceProvider.GetServices<INamedBrowserFactory>()
            .FirstOrDefault(f => f.Name == config.Driver.BrowserName && f.Type == config.Driver.BrowserType);

        if (factory == null)
        {
            throw new Exception(
                $"No factory registered for BrowserName: '{config.Driver.BrowserName}' and BrowserType:'{config.Driver.BrowserType}'.");
        }

        return factory.Create();
    }

    public void CloseDriver()
    {
        try
        {
            if (WebDriver != null && IsWebDriverCreated)
            {
                WebDriver.Quit();
                WebDriver.Dispose();
            }
            webDriverService = new Lazy<IWebDriver>(CreateWebDriver, true);
        }
        catch (Exception ex)
        {
            log.Error($"Error details: {ex}");
            throw new Exception($"Error occurred while closing driver. Please see logs for more details.", ex);
        }
    }

    public void NavigateToUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException("URL cannot be null or empty.");
        }

        WebDriver.Navigate().GoToUrl(url);
    }

    public void GoToTab(int tabIndex)
    {
        IList<string> tabs = WebDriver.WindowHandles;

        if (tabIndex < 0 || tabIndex >= tabs.Count)
        {
            throw new ArgumentOutOfRangeException("Tab index '{tabIndex}' is out of range.");
        }

        WebDriver.SwitchTo().Window(tabs[tabIndex]);
    }

    public void CloseCurrentTab()
    {
        ((IJavaScriptExecutor)WebDriver).ExecuteScript("window.close();");
        WebDriver.SwitchTo().Window(WebDriver.WindowHandles.Last());
    }

    public IWebElement FindElement(string xPath, int timeout = 10)
    {
        var element = GetElementFromDOM(xPath);
        if (element != null)
        {
            // Scroll to the item view if the advertisement exists
            ExecuteAsyncJSScriptForElement("arguments[0].scrollIntoView();", element);
            CheckClickabilityOfElement(xPath, timeout);
            return element;
        }

        throw new Exception($"Element with xPath '{xPath}' was not found.");

        //CheckClickabilityOfElement(xPath, timeout);
        //return WebDriver.FindElement(By.XPath(xPath));
    }

    public IWebElement FindElement(string xPath, string frameName, int timeout = 10)
    {
        log.Information($"Switch to frame '{frameName}' before try to find element.");
        WebDriver.SwitchTo().Frame(frameName);
        CheckClickabilityOfElement(xPath, timeout);
        var element = WebDriver.FindElement(By.XPath(xPath));
        WebDriver.SwitchTo().DefaultContent();
        return element;
    }

    public List<IWebElement> FindElements(string xPath, int timeout = 10)
    {
        CheckClickabilityOfElement(xPath, timeout);
        return WebDriver.FindElements(By.XPath(xPath)).ToList();
    }

    public void EnterText(string xPath, string tezt)
    {
        var elem = FindElement(xPath);
        elem.Clear();
        elem.SendKeys(tezt);
    }

    private void CheckClickabilityOfElement(string xPath, int timeout)
    {
        var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));

        try
        {
            wait.Until(x =>
            {
                var element = x.FindElement(By.XPath(xPath));
                return element.Displayed && element.Enabled;
            });
        }
        catch (WebDriverTimeoutException ex)
        {
            log.Error($"Error details: {ex}");
            throw new Exception($"Element with xPath '{xPath}' is not сlickable.", ex);
        }
        catch (Exception e)
        {
            log.Error($"Error details: {e}");
            throw new Exception($"An error occurred while searching for element with xPath '{xPath}'.", e);
        }
    }

    public bool IsElementVisibleOnPage(string xPath, int timeout = 5)
    {
        var isPresented = false;

        try
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));
            isPresented = wait.Until(x => x.FindElement(By.XPath(xPath)).Displayed);
        }
        catch (WebDriverTimeoutException ex)
        {
            log.Error($"Error details: {ex}");
            throw new Exception($"Element with xPath '{xPath}' was not visible within the specified timeout.", ex);
        }
        catch (Exception e)
        {
            log.Error($"Error details: {e}");
            throw new Exception($"An error occurred while searching for element with xPath '{xPath}'.", e);
        }

        return isPresented;
    }

    public IWebElement GetElementFromDOM(string xPath, int timeout = 10)
    {
        try
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));
            var element = wait.Until(x => x.FindElement(By.XPath(xPath)));
            return element;
        }
        catch (Exception e)
        {
            var errorMessage = $"Element with xPath '{xPath}' does not exist in the current DOM model.";
            log.Error(e.ToString());
            throw new Exception(errorMessage, e);
        }
    }

    public void MoveToElement(string xPath)
    {
        IsElementVisibleOnPage(xPath);
        var move = new OpenQA.Selenium.Interactions.Actions(WebDriver);
        move.MoveToElement(FindElement(xPath)).Perform();
    }

    public void DoubleClickToElement(string xPath)
    {
        var doubleClick = new OpenQA.Selenium.Interactions.Actions(WebDriver);
        doubleClick.DoubleClick(FindElement(xPath)).Perform();
    }

    public void RightClickToElement(string xPath)
    {
        var doubleClick = new OpenQA.Selenium.Interactions.Actions(WebDriver);
        doubleClick.ContextClick(FindElement(xPath)).Perform();
    }

    public void SelectFromDropDownByValue(string xPath, string value)
    {
        var dropDown = new SelectElement(FindElement(xPath));
        dropDown.SelectByValue(value);
    }

    public void SelectFromDropDownByText(string xPath, string text)
    {
        var dropDown = new SelectElement(FindElement(xPath));
        dropDown.SelectByText(text);
    }

    public void SelectFromDropDownByIndex(string xPath, int index)
    {
        var dropDown = new SelectElement(FindElement(xPath));
        dropDown.SelectByIndex(index);
    }

    public void WaitSomeSeconds(int timeInSeconds)
    {
        Thread.Sleep(timeInSeconds * 1000);
    }

    public void SaveScreenshot(string path)
    {
        ITakesScreenshot screen = WebDriver as ITakesScreenshot;
        Screenshot screenshot = screen.GetScreenshot();
        screenshot.SaveAsFile(path);
    }

    public byte[] GetScreenshot()
    {
        ITakesScreenshot screen = WebDriver as ITakesScreenshot;
        var screenshot = screen.GetScreenshot();
        return screenshot.AsByteArray;
    }

    public string GetBrowserLogs()
    {
        var browserLogs = WebDriver.Manage().Logs.GetLog(LogType.Browser);
        var browserLogsList = browserLogs.Select(x => x.ToString());
        var logs = string.Join('\n', browserLogsList);
        return logs;
    }

    public object ExecuteAsyncJSScriptForElement(string script, IWebElement element)
    {
        return ((IJavaScriptExecutor)WebDriver).ExecuteScript(script, element);
    }

    public object ExecuteAsyncJSScript(string script)
    {
        return ((IJavaScriptExecutor)WebDriver).ExecuteScript(script);
    }

    public object ExecuteJSScriptWithParam(string script, params object[] args)
    {
        return ((IJavaScriptExecutor)WebDriver).ExecuteScript(script, args);
    }
}
