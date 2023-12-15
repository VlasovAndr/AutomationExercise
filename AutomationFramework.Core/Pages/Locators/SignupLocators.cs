namespace AutomationFramework.Core.Pages.Locators;

public class SignupLocators
{
    public string LoginForm => $".//div[@class = 'login-form']";
    public string CreateAccountBtn => $"{LoginForm}//input[@data-qa = 'mobile_number']";
    public string CreateAccountMessage => $"//h2[@data-qa = 'account-created']/b";
    public string DeleteAccountMessage => $"//h2[@data-qa = 'account-deleted']/b";
    public string ContinueBtn => $"//a[@data-qa = 'continue-button']";

    #region ACCOUNT INFORMATION

    public string LoginFormTitle => $"{LoginForm}/h2";

    public string GenderRadioBtn(string gender) => $"{LoginForm}//input[@type = 'radio' and @value = '{gender}']";
    public string LoginNameField => $"{LoginForm}//input[@data-qa = 'name']";
    public string LoginEmailField => $"{LoginForm}//input[@data-qa = 'email']";
    public string LoginPasswordField => $"{LoginForm}//input[@data-qa = 'password']";

    public string DayOfBirthDropDown => $"{LoginForm}//select[@data-qa = 'days']";
    public string MonthOfBirthDropDown => $"{LoginForm}//select[@data-qa = 'months']";
    public string YearOfBirthDropDown => $"{LoginForm}//select[@data-qa = 'years']";

    public string NewsletterCheckBox => $"{LoginForm}//input[@id = 'newsletter']";
    public string SpecOfferCheckBox => $"{LoginForm}//input[@id = 'optin']";

    #endregion

    #region ADDRESS INFORMATION

    public string FirstNameField => $"{LoginForm}//input[@data-qa = 'first_name']";
    public string LastNameField => $"{LoginForm}//input[@data-qa = 'last_name']";
    public string CompanyField => $"{LoginForm}//input[@data-qa = 'company']";
    public string AddressField => $"{LoginForm}//input[@data-qa = 'address']";
    public string Address2Field => $"{LoginForm}//input[@data-qa = 'address2']";

    public string CountyDropDown => $"{LoginForm}//select[@data-qa = 'country']";

    public string StateField => $"{LoginForm}//input[@data-qa = 'state']";
    public string CityField => $"{LoginForm}//input[@data-qa = 'city']";
    public string ZipcodeField => $"{LoginForm}//input[@data-qa = 'zipcode']";
    public string MobileNumberField => $"{LoginForm}//input[@data-qa = 'mobile_number']";

    #endregion
}
