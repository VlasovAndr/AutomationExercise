using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;

namespace AutomationFramework.Core.Pages;

public class SignupAndLoginPage : PageBase
{
    public Header Header => header;

    protected override string PageName => "Signup / Login Page";
    protected override string PageUrl => $"{BaseUrl}/login";

    private readonly Header header;
    private readonly SignupAndLoginLocators repo;

    public SignupAndLoginPage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header, SignupAndLoginLocators repo)
        : base(browser, log, config)
    {
        this.header = header;
        this.repo = repo;
    }

    public string GetSignupFormTitle()
    {
        string formTitle = browser.FindElement(repo.SignupFormTitle).Text;
        LogPageInfo($"Signup form title: '{formTitle}'");

        return formTitle;
    }

    public void FillSignupForm(string name, string email)
    {
        browser.EnterText(repo.SignupNameField, name);
        browser.EnterText(repo.SignupEmailField, email);
        LogPageInfo($"Signup form filled");
    }

    public void ClickOnSignUpBtn()
    {
        browser.FindElement(repo.SignupBtn).Click();
        LogPageInfo($"Clicked on 'SignUp' button");
    }

    public string GetSignUpErrorMessage()
    {
        var errorMessage = browser.FindElement(repo.SignUpErrorMessage).Text;
        LogPageInfo($"Error message is {errorMessage}");

        return errorMessage;
    }

    public string GetLoginFormTitle()
    {
        string formTitle = browser.FindElement(repo.LoginFormTitle).Text;
        LogPageInfo($"Login form title: '{formTitle}'");

        return formTitle;
    }

    public void FillLoginForm(string email, string password)
    {
        browser.EnterText(repo.LoginEmailField, email);
        browser.EnterText(repo.LoginPasswordField, password);
        LogPageInfo($"Login form filled");
    }

    public void ClickOnLoginBtn()
    {
        browser.FindElement(repo.LoginBtn).Click();
        LogPageInfo($"Clicked on 'Login' button");
    }

    public string GetLoginFormErrorMessage()
    {
        var message = browser.FindElement(repo.LoginFormErrorMessage).Text;
        LogPageInfo($"Login form error message: {message}");

        return message;
    }
}
