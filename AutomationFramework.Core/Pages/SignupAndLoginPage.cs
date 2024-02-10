using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;
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

    public SignupAndLoginPage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header, SignupAndLoginLocators repo, ITestReporter reporter)
        : base(browser, log, config, reporter)
    {
        this.header = header;
        this.repo = repo;
    }

    [AllureStep($"|{pageName}| Getting signup form title")]
    public string GetSignupFormTitle()
    {
        string formTitle = browser.FindElement(repo.SignupFormTitle).Text;
        LogParameterInfo("Signup form title", formTitle);

        return formTitle;
    }

    [AllureStep($"|{pageName}| Filling signup form")]
    public void FillSignupForm(string name, string email)
    {
        browser.EnterText(repo.SignupNameField, name);
        browser.EnterText(repo.SignupEmailField, email);
    }

    [AllureStep($"|{pageName}| Submiting signup form")]
    public void SubmitSignupForm()
    {
        browser.FindElement(repo.SignupBtn).Click();
    }

    [AllureStep($"|{pageName}| Getting error message")]
    public string GetSignUpErrorMessage()
    {
        var errorMessage = browser.FindElement(repo.SignUpErrorMessage).Text;
        LogParameterInfo("Error message", errorMessage);

        return errorMessage;
    }

    [AllureStep($"|{pageName}| Getting login form title")]
    public string GetLoginFormTitle()
    {
        string formTitle = browser.FindElement(repo.LoginFormTitle).Text;
        LogParameterInfo("Login form title", formTitle);

        return formTitle;
    }

    [AllureStep($"|{pageName}| Filling login form")]
    public void FillLoginForm(string email, string password)
    {
        browser.EnterText(repo.LoginEmailField, email);
        browser.EnterText(repo.LoginPasswordField, password);
        LogParameterInfo("email", email);
    }
    [AllureStep($"|{pageName}| Submiting login form")]
    public void SubmitLoginForm()
    {
        browser.FindElement(repo.LoginBtn).Click();
    }

    [AllureStep($"|{pageName}| Getting login form error message")]
    public string GetLoginFormErrorMessage()
    {
        var message = browser.FindElement(repo.LoginFormErrorMessage).Text;
        LogParameterInfo("Login form error message", message);

        return message;
    }
}
