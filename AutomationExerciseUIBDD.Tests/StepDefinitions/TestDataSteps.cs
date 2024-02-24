using AutomationExerciseUIBDD.Tests.Dependencies;
using AutomationFramework.Common.Models;
using AutomationFramework.Common.Services;
using AutomationFramework.Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationExerciseUIBDD.Tests.StepDefinitions;

[Binding]
public class TestDataSteps 
{
	private ScenarioContext scenarioContext;
	private readonly DataGeneratorService generatorService;

	public TestDataSteps(ScenarioContext scenarioContext)
	{
		this.scenarioContext = scenarioContext;
		var container = DIContainerSpecflow.GetServiceProvider();
		generatorService = container.GetRequiredService<DataGeneratorService>();
	}

	[Given(@"some user")]
	public void GivenSomeUser()
	{
		User user = generatorService.GenerateRandomUser(newsletterInput: true, specialOffersInput: true);
		scenarioContext["user"] = user;
	}
}