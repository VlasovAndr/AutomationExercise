using AutomationFramework.Common.Abstractions;

namespace AutomationFramework.Core.Pages.SignupAndLoginPage;

public class SignupForm : FormBase
{
    protected override string ComponentName => "SignupForm";

    public SignupForm(IWebDriverWrapper browser, ILogging log, ITestReporter reporter)
        : base(browser, log, reporter)
    {
    }

    public override string FormContainer => $".//div[@class = 'signup-form']";
    public override string Title => $"{FormContainer}/h2";
    public override string EmailField => $"{FormContainer}//input[@data-qa = 'signup-name']";
    public override string PasswordField => $"{FormContainer}//input[@data-qa = 'signup-email']";
    public override string SubmitBtn => $".//button[@data-qa = 'signup-button']";
    public override string ErrorMessage => $"{FormContainer}//p";
}
