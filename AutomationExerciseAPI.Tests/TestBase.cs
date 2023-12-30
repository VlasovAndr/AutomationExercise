﻿using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Services;
using AutomationFramework.Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace AutomationExerciseAPI.Tests;

public class TestBase
{
    public IServiceProvider container;
    public ILogging log;
    private readonly CleanupTestService cleanupService;

    public TestBase()
    {
        container = DIContainer.ConfigureServices();
        log = container.GetRequiredService<ILogging>();
        cleanupService = container.GetRequiredService<CleanupTestService>();
    }

    [TearDown]
    public void AfterEach()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;

        if (outcome == TestStatus.Passed)
        {
            log.Information("Outcome: Passed");
        }
        else if (outcome == TestStatus.Failed)
        {
            log.Error($"Test failed for reason: {TestContext.CurrentContext.Result.Message}");
        }
        else
        {
            log.Warning("Outcome: " + outcome);
        }

        log.Information("--------------------------------------");
        log.Information("Starting tearsown");
        log.Information("--------------------------------------");

        Cleanup();
    }

    private void Cleanup()
    {
        log.Information("--------------------------------------");
        log.Information("Starting Cleanup");
        log.Information("--------------------------------------");
        cleanupService.Cleanup();
    }
}
