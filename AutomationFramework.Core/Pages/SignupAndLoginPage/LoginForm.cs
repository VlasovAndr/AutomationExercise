using AutomationFramework.Common.Abstractions;

namespace AutomationFramework.Core.Pages.SignupAndLoginPage;

public class LoginForm : FormBase
{
    protected override string ComponentName => "LoginForm";

    public LoginForm(IWebDriverWrapper browser, ILogging log, ITestReporter reporter)
        : base(browser, log, reporter)
    {
    }

    public override string FormContainer => $".//div[@class = 'login-form']";
    public override string Title => $"{FormContainer}/h2";
    public override string EmailField => $"{FormContainer}//input[@data-qa = 'login-email']";
    public override string PasswordField => $"{FormContainer}//input[@data-qa = 'login-password']";
    public override string SubmitBtn => $".//button[@data-qa = 'login-button']";
    public override string ErrorMessage => $"{FormContainer}//p";
}
