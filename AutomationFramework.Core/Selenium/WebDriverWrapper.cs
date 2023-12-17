using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
            log.Error("Error occurred while closing driver. Message: " + ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public void NavigateToUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url), "URL cannot be null or empty.");
        }

        WebDriver.Navigate().GoToUrl(url);
    }

    public void GoToTab(int tabIndex)
    {
        IList<string> tabs = WebDriver.WindowHandles;

        if (tabIndex < 0 || tabIndex >= tabs.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(tabIndex), $"Tab index '{tabIndex}' is out of range.");
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
            ExecuteAsyncJSScriptForElement("arguments[0].scrollIntoView();", element);
            CheckClickabilityOfElement(xPath, timeout);
            return element;
        }

        throw new Exception($"Element with xPath '{xPath}' was not found.");
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
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xPath)));
            return;
        }
        catch (WebDriverTimeoutException)
        {
            log.Error($"Element with xPath '{xPath}' is not сlickable.");
        }
        catch (Exception e)
        {
            log.Error($"An error occurred while searching for element with xPath '{xPath}'. Exception: {e.Message}");
            throw;
        }

        throw new Exception($"Element with xPath '{xPath}' is not сlickable.");
    }

    public bool IsElementVisibleOnPage(string xPath, int timeout = 10)
    {
        var isPresented = false;

        try
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
            isPresented = true;
        }
        catch (WebDriverTimeoutException)
        {
            log.Error($"Element with xPath '{xPath}' was not visible within the specified timeout.");
        }
        catch (Exception e)
        {
            log.Error($"An error occurred while searching for element with xPath '{xPath}'. Exception: {e.Message}");
        }

        return isPresented;
    }

    public IWebElement GetElementFromDOM(string xPath, int timeout = 10)
    {
        try
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementExists(By.XPath(xPath)));
        }
        catch (WebDriverTimeoutException e)
        {
            log.Error($"Element with xPath '{xPath}' is not exist in the current DOM model. Exception: {e.Message}");
            throw;
        }
        catch (Exception e)
        {
            log.Error($"An error occurred while searching for element with xPath '{xPath}'. Exception: {e.Message}");
            throw;
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

    public void TakeScreenshot(string path)
    {
        ITakesScreenshot screen = WebDriver as ITakesScreenshot;
        Screenshot screenshot = screen.GetScreenshot();
        screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
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
