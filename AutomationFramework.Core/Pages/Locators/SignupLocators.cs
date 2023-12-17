namespace AutomationFramework.Core.Pages.Locators;

public class SignupLocators
{
    public string SignupForm => $".//div[@class = 'Signup-form']";
    public string CreateAccountBtn => $"{SignupForm}//button[@data-qa = 'create-account']";
    public string CreateAccountMessage => $"//h2[@data-qa = 'account-created']/b";
    public string DeleteAccountMessage => $"//h2[@data-qa = 'account-deleted']/b";
    public string ContinueBtn => $"//a[@data-qa = 'continue-button']";

    #region ACCOUNT INFORMATION

    public string SignupFormTitle => $"{SignupForm}/h2";

    public string GenderRadioBtn(string gender) => $"{SignupForm}//input[@type = 'radio' and @value = '{gender}']";
    public string SignupNameField => $"{SignupForm}//input[@data-qa = 'name']";
    public string SignupEmailField => $"{SignupForm}//input[@data-qa = 'email']";
    public string SignupPasswordField => $"{SignupForm}//input[@data-qa = 'password']";

    public string DayOfBirthDropDown => $"{SignupForm}//select[@data-qa = 'days']";
    public string MonthOfBirthDropDown => $"{SignupForm}//select[@data-qa = 'months']";
    public string YearOfBirthDropDown => $"{SignupForm}//select[@data-qa = 'years']";

    public string NewsletterCheckBox => $"{SignupForm}//input[@id = 'newsletter']";
    public string SpecOfferCheckBox => $"{SignupForm}//input[@id = 'optin']";

    #endregion

    #region ADDRESS INFORMATION

    public string FirstNameField => $"{SignupForm}//input[@data-qa = 'first_name']";
    public string LastNameField => $"{SignupForm}//input[@data-qa = 'last_name']";
    public string CompanyField => $"{SignupForm}//input[@data-qa = 'company']";
    public string AddressField => $"{SignupForm}//input[@data-qa = 'address']";
    public string Address2Field => $"{SignupForm}//input[@data-qa = 'address2']";

    public string CountyDropDown => $"{SignupForm}//select[@data-qa = 'country']";

    public string StateField => $"{SignupForm}//input[@data-qa = 'state']";
    public string CityField => $"{SignupForm}//input[@data-qa = 'city']";
    public string ZipcodeField => $"{SignupForm}//input[@data-qa = 'zipcode']";
    public string MobileNumberField => $"{SignupForm}//input[@data-qa = 'mobile_number']";

    #endregion
}
