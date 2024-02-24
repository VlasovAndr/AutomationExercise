using AutomationExerciseUIBDD.Tests.Dependencies;
using AutomationFramework.Core.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationExerciseUIBDD.Tests.StepDefinitions;

[Binding]
public class HeaderSteps
{
	private readonly Header header;

	public HeaderSteps()
	{
		var container = DIContainerSpecflow.GetServiceProvider();
		header = container.GetRequiredService<Header>();
	}

	[When(@"I go to Signup and Login header menu")]
	public void OpenSignupLoginHeaderMenu()
	{
		header.GoToSignupLoginMenu();
	}
}