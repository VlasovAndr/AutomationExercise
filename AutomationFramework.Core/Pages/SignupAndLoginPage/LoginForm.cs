using AutomationFramework.Common.Abstractions;

namespace AutomationFramework.Core.Pages.SignupAndLoginPage;

public class LoginForm : FormBase
{
    protected override string ComponentName => "LoginForm";

    public LoginForm(IWebDriverWrapper browser, ILogging log, ITestReporter reporter)
        : base(browser, log, reporter)
    {
    }

    protected override string FormContainer => $".//div[@class = 'login-form']";
    protected override string Title => $"{FormContainer}/h2";
    protected override string EmailField => $"{FormContainer}//input[@data-qa = 'login-email']";
    protected override string PasswordField => $"{FormContainer}//input[@data-qa = 'login-password']";
    protected override string SubmitBtn => $".//button[@data-qa = 'login-button']";
    protected override string ErrorMessage => $"{FormContainer}//p";
}
