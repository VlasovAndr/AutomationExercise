using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Variables;
using AutomationFramework.Common.Reports;
using AutomationFramework.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutomationFramework.Core.Selenium.WebDriverFactory;
using AutomationFramework.Core.Selenium;
using AutomationFramework.Core.Pages;
using AutomationFramework.Core.Pages.Locators;

namespace AutomationFramework.Core.Dependencies;

public class DIContainer
{
    public static IServiceCollection serviceCollection;

    public static IServiceProvider ConfigureServices()
    {
        if (serviceCollection == null)
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<DefaultVariables>();
            serviceCollection.AddSingleton<ILogging, ConsoleLogger>(); // [ConsoleLogger/FileLogger/SpecflowLogger/]
            serviceCollection.AddSingleton<TestRunConfiguration>();
            serviceCollection.AddSingleton<INamedBrowserFactory, ChromeDriverFactory>();
            serviceCollection.AddSingleton<INamedBrowserFactory, FirefoxDriverFactory>();
            serviceCollection.AddSingleton<INamedBrowserFactory, RemoteChromeDriverFactory>();
            serviceCollection.AddSingleton<INamedBrowserFactory, RemoteFirefoxDriverFactory>();
            serviceCollection.AddSingleton<IWebDriverWrapper, WebDriverWrapper>();

            serviceCollection.AddSingleton<HomePage>();
            serviceCollection.AddSingleton<SignupAndLoginPage>();
            serviceCollection.AddSingleton<SignupPage>();
            serviceCollection.AddSingleton<Header>();

            serviceCollection.AddSingleton<HeaderLocators>();
            serviceCollection.AddSingleton<SignupAndLoginLocators>();
            serviceCollection.AddSingleton<SignupLocators>();

        }

        return serviceCollection.BuildServiceProvider();
    }
}
