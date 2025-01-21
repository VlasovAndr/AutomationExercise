using AutomationExerciseUIBDD.Tests.Dependencies;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Pages.SignupAndLoginPage;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationExerciseUIBDD.Tests.StepDefinitions;

[Binding]
public class SignupAndLoginPageSteps
{
	private readonly SignupAndLoginPage signupAndLoginPage;
	private ScenarioContext scenarioContext;

	public SignupAndLoginPageSteps(ScenarioContext scenarioContext)
	{
		this.scenarioContext = scenarioContext;
		var container = DIContainerSpecflow.GetServiceProvider();
		signupAndLoginPage = container.GetRequiredService<SignupAndLoginPage>();
	}

	[When(@"I fill Signup form for previously created user on Signup and Login page")]
	public void FillSignupFormForPreviouslyCreatedUser()
	{
		var user = (User)scenarioContext["user"];
		signupAndLoginPage.FillSignupForm(user.Account.Name, user.Account.Email);
	}

	[When(@"I submit Signup form on Signup and Login page")]
	public void SubmitSignupForm()
	{
		signupAndLoginPage.SubmitSignupForm();
	}

	[Then(@"I validate Signup form title is (.*) on Signup and Login page")]
	public void ValidateSignupFormTitle(string title)
	{
		signupAndLoginPage.GetSignupFormTitle().Should().Be(title);
	}
}