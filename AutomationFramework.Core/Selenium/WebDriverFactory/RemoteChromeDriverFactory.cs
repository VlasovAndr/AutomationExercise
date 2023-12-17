using AutomationFramework.Common.Abstractions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using AutomationFramework.Core.Configuration;
using System.Drawing;

namespace AutomationFramework.Core.Selenium.WebDriverFactory;

public class RemoteChromeDriverFactory : INamedBrowserFactory
{
    public BrowserName Name => BrowserName.Chrome;
    public BrowserType Type => BrowserType.Remote;
    private TestRunConfiguration testRunConfiguration;
    private ILogging log;

    public RemoteChromeDriverFactory(TestRunConfiguration testRunConfiguration, ILogging log)
    {
        this.testRunConfiguration = testRunConfiguration;
        this.log = log;
    }

    public IWebDriver Create()
    {
        log.Debug("Creating Remote Chrome Driver");

        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
        options.AddUserProfilePreference("browser.download.manager.showWhenStarting", false);
        options.AddUserProfilePreference("safebrowsing.enabled", "true");
        options.AddArgument("no-sandbox");
        options.AddArgument("--disable-gpu");
        options.AddArgument("disable-popup-blocking");
        options.AddUserProfilePreference("download.default_directory", testRunConfiguration.Framework.DownloadedLocation);
        options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
        options.AddArgument("disable-notifications");
        options.AddUserProfilePreference("autofill.profile_enabled", false);
        
        if (testRunConfiguration.Driver.Headless) options.AddArgument("--headless=new");

        options.AddAdditionalOption("selenoid:options", new Dictionary<string, object>
        {
            ["enableLog"] = true,
            ["enableVnc"] = true,
            ["enableVideo"] = false
        });

        var webDriver = new RemoteWebDriver(new Uri($"{testRunConfiguration.Driver.GridHubUrl}"), options.ToCapabilities());
        //webDriver.Manage().Window.Maximize();
        webDriver.Manage().Window.Size = new Size(1920, 1080);

        return webDriver;
    }
}
