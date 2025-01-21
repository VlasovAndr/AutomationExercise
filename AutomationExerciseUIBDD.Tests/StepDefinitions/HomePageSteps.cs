using AutomationExerciseUIBDD.Tests.Dependencies;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Pages.HomePage;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationExerciseUIBDD.Tests.StepDefinitions;

[Binding]
public class HomePageSteps
{
	private readonly HomePage homePage;
	private ScenarioContext scenarioContext;

	public HomePageSteps(ScenarioContext scenarioContext)
	{
		this.scenarioContext = scenarioContext;
		var container = DIContainerSpecflow.GetServiceProvider();
		homePage = container.GetRequiredService<HomePage>();
	}

	[When(@"I open home page")]
	public void OpenHomepage()
	{
		homePage.Open();
	}

	[When(@"I click on delete account header menu")]
	public void ClickDeleteAccount()
	{
		homePage.Header.ClickOnDeleteAccountMenu();
	}

	[Then(@"I validate home page is opened")]
	public void ThenIValidateHomePageIsOpened()
	{
		homePage.IsPageOpened().Should().BeTrue();
	}

	[Then(@"I validate Logged in header is visible")]
	public void ValidateLoggedInHeader()
	{
		var user = (User)scenarioContext["user"];

		homePage.Header.GetAllHeadersText()
			.Any(x => x.Contains($"Logged in as {user.Account.Name}"))
			.Should().BeTrue();
	}
}