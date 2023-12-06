using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AutomationFramework.Core.Selenium.WebDriverFactory;

public class FirefoxDriverFactory : INamedBrowserFactory
{
    public BrowserName Name => BrowserName.Firefox;
    public BrowserType Type => BrowserType.Local;
    private TestRunConfiguration testRunConfiguration;
    private ILogging log;

    public FirefoxDriverFactory(TestRunConfiguration testRunConfiguration, ILogging log)
    {
        this.testRunConfiguration = testRunConfiguration;
        this.log = log;
    }

    public IWebDriver Create()
    {
        log.Debug("Creating FirefoxDriver");

        var options = new FirefoxOptions();
        options.SetPreference("browser.download.prompt_for_download", false);
        options.SetPreference("pdfjs.disabled", true);  // to always open PDF externally
        options.SetPreference("browser.download.manager.showWhenStarting", false);
        options.SetPreference("browser.safebrowsing.enabled", true);
        options.AddArgument("no-sandbox");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--disable-popup-blocking");
        options.SetPreference("browser.download.dir", testRunConfiguration.Framework.DownloadedLocation);
        options.SetPreference("network.cookie.cookieBehavior", 0);
        if (testRunConfiguration.Driver.Headless) options.AddArgument("--headless=new");

        var specificDriver = new FirefoxDriver(options);

        //specificDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //specificDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        //specificDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
        specificDriver.Manage().Window.Maximize();

        return specificDriver;
    }
}