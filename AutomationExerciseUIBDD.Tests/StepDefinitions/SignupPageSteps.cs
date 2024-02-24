using AutomationExerciseUIBDD.Tests.Dependencies;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationExerciseUIBDD.Tests.StepDefinitions;

[Binding]
public class SignupPageSteps
{
	private readonly SignupPage signupPage;
	private ScenarioContext scenarioContext;

	public SignupPageSteps(ScenarioContext scenarioContext)
	{
		this.scenarioContext = scenarioContext;
		var container = DIContainerSpecflow.GetServiceProvider();
		signupPage = container.GetRequiredService<SignupPage>();
	}

	[When(@"I fill Account Info form for previously created user on Signup page")]
	public void FillAccountInfoFormForPreviouslyCreatedUser()
	{
		var user = (User)scenarioContext["user"];
		signupPage.FillAccountInfoForm(user.Account);
	}

	[When(@"I fill Address Info form for previously created user on Signup page")]
	public void FillAddressInfoFormForPreviouslyCreatedUser()
	{
		var user = (User)scenarioContext["user"];
		signupPage.FillAddressInfoForm(user.Address);
	}

	[When(@"I submit Signup form on Signup page")]
	public void SubmitSignupForm()
	{
		signupPage.SubmitSignupForm();
	}

	[When(@"I click continue button on Signup page")]
	public void ClickContinueButton()
	{
		signupPage.ClickOnContinueBtn();
	}

	[Then(@"I validate Signup form title is (.*) on Signup page")]
	public void ValidateSignupFormTitle(string title)
	{
		signupPage.GetSignupFormTitle().Should().Be(title);
	}

	[Then(@"I validate account creation message is (.*) on Signup page")]
	public void ValidateAccountCreationMessage(string message)
	{
		signupPage.GetAccountCreatedMessage().Should().Be(message);
	}

	[Then(@"I validate account deleted message is (.*) on Signup page")]
	public void ValidateAccountDeletedMessage(string message)
	{
		signupPage.GetAccountDeletedMessage().Should().Be(message);
	}
}