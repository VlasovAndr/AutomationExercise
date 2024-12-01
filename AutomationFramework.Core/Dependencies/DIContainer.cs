using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Variables;
using AutomationFramework.Common.Reports;
using AutomationFramework.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutomationFramework.Core.Pages;
using AutomationFramework.Core.Pages.Locators;
using AutomationFramework.Core.Steps;
using AutomationFramework.Common.Services.API;
using AutomationFramework.Common.Services;
using Microsoft.Playwright;
using Header = AutomationFramework.Core.Pages.Header;

namespace AutomationFramework.Core.Dependencies;

public sealed class DIContainer
{
    private IServiceCollection _serviceCollection;

    public DIContainer()
    {
        RegisterDefaultServices();
    }

    private void RegisterDefaultServices()
    {
        _serviceCollection = new ServiceCollection();

        _serviceCollection.AddSingleton<DefaultVariables>();
        _serviceCollection.AddSingleton<TestRunConfiguration>();
        _serviceCollection.AddScoped<ILogging, ConsoleLogger>();  // [ConsoleLogger/FileLogger/SpecflowLogger]
        _serviceCollection.AddScoped<ITestReporter, AllureReporter>();

        _serviceCollection.AddScoped<HomePage>();
        _serviceCollection.AddScoped<SignupAndLoginPage>();
        _serviceCollection.AddScoped<SignupPage>();
        _serviceCollection.AddScoped<Header>();
        _serviceCollection.AddScoped<ContactUsPage>();

        _serviceCollection.AddTransient<HeaderLocators>();
        _serviceCollection.AddTransient<SignupAndLoginLocators>();
        _serviceCollection.AddTransient<SignupLocators>();
        _serviceCollection.AddTransient<ContactUsLocators>();

        _serviceCollection.AddScoped<APIClient>();
        _serviceCollection.AddScoped<UserAPIService>();
        _serviceCollection.AddScoped<LoginAPIService>();

        _serviceCollection.AddScoped<UserAPIPlaywrightService>();
        _serviceCollection.AddScoped<LoginAPIPlaywrightService>();
        _serviceCollection.AddScoped<CleanupPlaywrightTestService>();

        _serviceCollection.AddKeyedScoped<IUserSteps, UserUISteps>("UI");
        _serviceCollection.AddKeyedScoped<IUserSteps, UserAPISteps>("API");

        _serviceCollection.AddScoped<CleanupTestService>();
        _serviceCollection.AddTransient<DataGeneratorService>();
    }

    public DIContainer RegisterPlaywrightService(IPage page)
    {
        _serviceCollection.AddScoped(x => page);
        return this;
    }

    public DIContainer RegisterPlaywrightAPIService(IAPIRequestContext request)
    {
        _serviceCollection.AddScoped(x => request);
        return this;
    }

    public IServiceProvider Build()
    {
        return _serviceCollection.BuildServiceProvider();
    }
}