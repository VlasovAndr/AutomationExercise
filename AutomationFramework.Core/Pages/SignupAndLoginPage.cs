using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;
using Microsoft.Playwright;
using NUnit.Allure.Attributes;

namespace AutomationFramework.Core.Pages;

public class SignupAndLoginPage : PageBase
{
    public Header Header => header;

    protected override string PageName => pageName;
    protected override string PageUrl => $"{BaseUrl}/login";

    private readonly Header header;
    private readonly SignupAndLoginLocators repo;
    private const string pageName = "Signup / Login Page";

    public SignupAndLoginPage(IPage page, ILogging log, TestRunConfiguration config, Header header, SignupAndLoginLocators repo, ITestReporter reporter)
        : base(page, config, reporter)
    {
        this.header = header;
        this.repo = repo;
    }

    [AllureStep($"|{pageName}| Getting signup form title")]
    public async Task<string> GetSignupFormTitle()
    {
        var formTitle = await Page.Locator(repo.SignupFormTitle).TextContentAsync();

        LogParameterInfo("Signup form title", formTitle);

        return formTitle;
    }

    [AllureStep($"|{pageName}| Filling signup form")]
    public async Task FillSignupForm(string name, string email)
    {
        await Page.Locator(repo.SignupNameField).FillAsync(name);
        await Page.Locator(repo.SignupEmailField).FillAsync(email);
    }

    [AllureStep($"|{pageName}| Submiting signup form")]
    public async Task SubmitSignupForm()
    {
        await Page.Locator(repo.SignupBtn).ClickAsync();

    }

    [AllureStep($"|{pageName}| Getting error message")]
    public async Task<string> GetSignUpErrorMessage()
    {
        var errorMessage = await Page.Locator(repo.SignUpErrorMessage).TextContentAsync();

        LogParameterInfo("Error message", errorMessage);

        return errorMessage;
    }

    [AllureStep($"|{pageName}| Getting login form title")]
    public async Task<string> GetLoginFormTitle()
    {
        var formTitle = await Page.Locator(repo.LoginFormTitle).TextContentAsync();

        LogParameterInfo("Login form title", formTitle);

        return formTitle;
    }

    [AllureStep($"|{pageName}| Filling login form")]
    public async Task FillLoginForm(string email, string password)
    {
        await Page.Locator(repo.LoginEmailField).FillAsync(email);
        await Page.Locator(repo.LoginPasswordField).FillAsync(password);
        LogParameterInfo("email", email);
    }

    [AllureStep($"|{pageName}| Submiting login form")]
    public async Task SubmitLoginForm()
    {
        await Page.Locator(repo.LoginBtn).ClickAsync();
    }

    [AllureStep($"|{pageName}| Getting login form error message")]
    public async Task<string> GetLoginFormErrorMessage()
    {
        var message = await Page.Locator(repo.LoginFormErrorMessage).TextContentAsync();

        LogParameterInfo("Login form error message", message);

        return message;
    }
}
