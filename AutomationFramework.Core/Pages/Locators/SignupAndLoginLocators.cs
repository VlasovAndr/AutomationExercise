namespace AutomationFramework.Core.Pages.Locators;

public class SignupAndLoginLocators
{
    public string SignupForm => $".//div[@class = 'signup-form']";
    public string SignupFormTitle => $"{SignupForm}/h2";
    public string SignupNameField => $"{SignupForm}//input[@data-qa = 'signup-name']";
    public string SignupEmailField => $"{SignupForm}//input[@data-qa = 'signup-email']";
    public string SignupBtn => $".//button[@data-qa = 'signup-button']";

    public string LoginForm => $".//div[@class = 'login-form']";
    public string LoginFormTitle => $"{LoginForm}/h2";
    public string LoginEmailField => $"{LoginForm}//input[@data-qa = 'login-email']";
    public string LoginPasswordField => $"{LoginForm}//input[@data-qa = 'login-password']";
    public string LoginBtn => $".//button[@data-qa = 'login-button']";
    public string LoginFormErrorMessage => $"{LoginForm}//p";
}
