using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace AutomationExercise.Tests;

public class TestBase
{
    public IServiceProvider container;
    public ILogging log;

    public TestBase()
    {
        container = DIContainer.ConfigureServices();
        log = container.GetRequiredService<ILogging>();
        var config = container.GetRequiredService<TestRunConfiguration>();
        log.SetLogLevel(config.Framework.LogLevel);
    }

    [TearDown]
    public void AfterEach()
    {
        log.Debug("Starting AfterEach teardown");

        var webDriver = container.GetRequiredService<IWebDriverWrapper>();
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;

        if (outcome == TestStatus.Passed)
        {
            log.Information("Outcome: Passed");
        }
        else if (outcome == TestStatus.Failed)
        {
            log.Error($"Test failed for reason: {TestContext.CurrentContext.Result.Message}");

            //Screenshot screenshot = (webDriver.WebDriver as ITakesScreenshot).GetScreenshot();
            //screenshot.SaveAsFile("screenshot.png", ScreenshotImageFormat.Png);
            //AllureLifecycle.Instance.AddAttachment("image1.png", "image/png", screenshot.AsByteArray);
        }
        else
        {
            log.Warning("Outcome: " + outcome);
        }

        try
        {
            if (webDriver.IsWebDriverCreated)
                webDriver.CloseDriver();
        }
        catch (Exception ex)
        {
            log.Error($"Failed on closing web driver on after test run event. {ex.Message}");
        }
    }
}
