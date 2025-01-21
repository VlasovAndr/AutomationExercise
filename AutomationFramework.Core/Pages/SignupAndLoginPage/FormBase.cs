using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Pages.Components;

namespace AutomationFramework.Core.Pages.SignupAndLoginPage;

public abstract class FormBase : ComponentBase
{
    public virtual string FormContainer { get; }
    public virtual string Title { get; }
    public virtual string EmailField { get; }
    public virtual string PasswordField { get; }
    public virtual string SubmitBtn { get; }
    public virtual string ErrorMessage { get; }

    public FormBase(IWebDriverWrapper browser, ILogging log, ITestReporter reporter)
        : base(browser, log, reporter)
    {
    }

    public string GetTitle()
    {
        var formTitle = string.Empty;

        CreateStep("Getting title", () =>
        {
            formTitle = browser.FindElement(Title).Text;
            LogParameterInfo("Form title", formTitle);
        });

        return formTitle;
    }

    public void Fill(string email, string password)
    {
        CreateStep("Filling form", () =>
        {
            browser.EnterText(EmailField, email);
            browser.EnterText(PasswordField, password);
            LogParameterInfo("Email", email);
        });
    }

    public void Submit()
    {
        CreateStep("Submiting", () =>
        {
            browser.FindElement(SubmitBtn).Click();
        });
    }

    public string GetErrorMessage()
    {
        var errorMessage = string.Empty;

        CreateStep("Getting error message", () =>
        {
            errorMessage = browser.FindElement(ErrorMessage).Text;
            LogParameterInfo("Error message", errorMessage);
        });

        return errorMessage;
    }
}
