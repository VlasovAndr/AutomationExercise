using AutomationFramework.Common.Abstractions;

namespace AutomationFramework.Core.Pages.SignupAndLoginPage;

public class SignupForm : FormBase
{
    protected override string ComponentName => "SignupForm";

    public SignupForm(IWebDriverWrapper browser, ILogging log, ITestReporter reporter)
        : base(browser, log, reporter)
    {
    }

    protected override string FormContainer => $".//div[@class = 'signup-form']";
    protected override string Title => $"{FormContainer}/h2";
    protected override string EmailField => $"{FormContainer}//input[@data-qa = 'signup-name']";
    protected override string PasswordField => $"{FormContainer}//input[@data-qa = 'signup-email']";
    protected override string SubmitBtn => $".//button[@data-qa = 'signup-button']";
    protected override string ErrorMessage => $"{FormContainer}//p";
}
