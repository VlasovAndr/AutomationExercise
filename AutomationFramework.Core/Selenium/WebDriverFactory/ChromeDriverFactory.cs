using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationFramework.Core.Selenium.WebDriverFactory;

public class ChromeDriverFactory : INamedBrowserFactory
{
    public BrowserName Name => BrowserName.Chrome;
    public BrowserType Type => BrowserType.Local;
    private TestRunConfiguration testRunConfiguration;
    private ILogging log;

    public ChromeDriverFactory(TestRunConfiguration testRunConfiguration, ILogging log)
    {
        this.testRunConfiguration = testRunConfiguration;
        this.log = log;
    }

    public IWebDriver Create()
    {
        log.Debug("Creating ChromeDriver");

        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
        options.AddUserProfilePreference("browser.download.manager.showWhenStarting", false);
        options.AddUserProfilePreference("safebrowsing.enabled", "true");
        //"no-sandbox" and "--disable-gpu" must work together. Or it will need to delete.
        //"no-sandbox" parameter is usefull for running in container 
        options.AddArgument("no-sandbox");
        options.AddArgument("--disable-gpu");
        options.AddArgument("disable-popup-blocking");
        options.AddUserProfilePreference("download.default_directory", testRunConfiguration.Framework.DownloadedLocation);
        options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
        if (testRunConfiguration.Driver.Headless) options.AddArgument("--headless=new");

        var webDriver = new ChromeDriver(options);

        //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        //webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
        webDriver.Manage().Window.Maximize();

        return webDriver;
    }
}
