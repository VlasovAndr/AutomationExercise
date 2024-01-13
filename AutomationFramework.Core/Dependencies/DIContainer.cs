using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Variables;
using AutomationFramework.Common.Reports;
using AutomationFramework.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutomationFramework.Core.Selenium.WebDriverFactory;
using AutomationFramework.Core.Selenium;
using AutomationFramework.Core.Pages;
using AutomationFramework.Core.Pages.Locators;
using AutomationFramework.Core.Steps;
using AutomationFramework.Common.Services.API;
using AutomationFramework.Common.Services;

namespace AutomationFramework.Core.Dependencies;

public class DIContainer
{
    private static IServiceCollection serviceCollection;

    public static IServiceProvider ConfigureServices()
    {
        if (serviceCollection == null)
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<DefaultVariables>();
            serviceCollection.AddScoped<ILogging, ConsoleLogger>(); // [ConsoleLogger/FileLogger/SpecflowLogger/]
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
            serviceCollection.AddSingleton<ContactUsPage>();

            serviceCollection.AddSingleton<HeaderLocators>();
            serviceCollection.AddSingleton<SignupAndLoginLocators>();
            serviceCollection.AddSingleton<SignupLocators>();
            serviceCollection.AddSingleton<ContactUsLocators>();

            serviceCollection.AddTransient<APIClient>();
            serviceCollection.AddTransient<UserAPIService>();

            serviceCollection.AddKeyedSingleton<IUserSteps, UserUISteps>("UI");
            serviceCollection.AddKeyedSingleton<IUserSteps, UserAPISteps>("API");

            serviceCollection.AddSingleton<CleanupTestService>();
            serviceCollection.AddSingleton<DataGeneratorService>();
        }

        return serviceCollection.BuildServiceProvider();
    }
}
