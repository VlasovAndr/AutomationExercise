using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Variables;
using AutomationFramework.Common.Reports;
using AutomationFramework.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutomationFramework.Core.Selenium.WebDriverFactory;
using AutomationFramework.Core.Selenium;

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

        }

        return serviceCollection.BuildServiceProvider();
    }
}
