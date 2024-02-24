using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Text;
using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services;
using NUnit.Framework.Interfaces;
using Allure.Net.Commons;
using AutomationExerciseUIBDD.Tests.Dependencies;

namespace AutomationExerciseUIBDD.Tests.GlobalHooks;

[Binding]
public class SpecflowTestBase
{
	public IServiceProvider container;
	public readonly ILogging log;
	private readonly CleanupTestService cleanupService;
	private readonly ITestReporter reporter;

	public SpecflowTestBase()
	{
		container = DIContainerSpecflow.GetServiceProvider();
		log = container.GetRequiredService<ILogging>();
		cleanupService = container.GetRequiredService<CleanupTestService>();
		reporter = container.GetRequiredService<ITestReporter>();
	}

	[BeforeTestRun]
	public static void Init()
	{
		DIContainerSpecflow.ConfigureService();
		AllureLifecycle.Instance.CleanupResultDirectory();
	}

	[AfterScenario]
	public void AfterEach()
	{
		var outcome = TestContext.CurrentContext.Result.Outcome.Status;
		var webDriver = container.GetRequiredService<IWebDriverWrapper>();

		if (outcome == TestStatus.Passed)
		{
			log.Information("Outcome: Passed");
		}
		else if (outcome == TestStatus.Failed)
		{
			log.Error($"Test failed for reason: {TestContext.CurrentContext.Result.Message}");
			var screenshot = webDriver.GetScreenshot();
			var browserLogs = webDriver.GetBrowserLogs();
			var pageSource = webDriver.WebDriver.PageSource;
			reporter.AddAttachment("errorScreenshot.png", "image/png", screenshot);
			reporter.AddAttachment("browserLogs.txt", "text/plain", Encoding.ASCII.GetBytes(browserLogs));
			reporter.AddAttachment("pageSource.html", "text/html", Encoding.ASCII.GetBytes(pageSource));
		}
		else
		{
			log.Warning("Outcome: " + outcome);
		}

		log.Information("--------------------------------------");
		log.Information("Starting teardown");
		log.Information("--------------------------------------");

		try
		{
			if (webDriver.IsWebDriverCreated)
				webDriver.CloseDriver();
		}
		catch (Exception ex)
		{
			log.Error($"Failed on closing web driver on after test run event. {ex.Message}");
		}

		cleanupService.Cleanup();
	}
}
