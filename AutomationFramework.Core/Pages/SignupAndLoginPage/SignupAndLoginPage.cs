using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Common.Header;

namespace AutomationFramework.Core.Pages.SignupAndLoginPage;

public class SignupAndLoginPage : PageBase
{
    public Header Header { get; }
    public LoginForm LoginForm { get; }
    public SignupForm SignupForm { get; }

    protected override string PageName => "Signup / Login Page";
    protected override string PageUrl => $"{BaseUrl}/login";

    public SignupAndLoginPage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config,
        Header header, ITestReporter reporter, LoginForm loginForm, SignupForm signupForm)
        : base(browser, log, config, reporter)
    {
        Header = header;
        LoginForm = loginForm;
        SignupForm = signupForm;
    }
}
