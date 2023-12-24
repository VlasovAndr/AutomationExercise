using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;

namespace AutomationFramework.Core.Pages;

public class SignupPage : PageBase
{
    private const string PAGE_NAME = "Signup Page";
    private string PageUrl => $"{BaseUrl}/signup";

    private readonly Header header;
    private readonly SignupLocators repo;

    public Header Header => header;

    public SignupPage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header, SignupLocators repo)
        : base(browser, log, config)
    {
        this.header = header;
        this.repo = repo;
    }

    public void Open()
    {
        browser.NavigateToUrl(PageUrl);
        log.Information($"|{PAGE_NAME}| {PAGE_NAME} is opened");
    }

    public string GetSignupFormTitle()
    {
        string formTitle = browser.FindElement(repo.SignupFormTitle).Text;
        log.Information($"|{PAGE_NAME}| Signup form title: '{formTitle}'");
        
        return formTitle;
    }

    public void FillSignupForm(AccountInfo accountInfo, AddressInfo addressInfo)
    {
        FillAccountInfoForm(accountInfo);
        FillAddressInfoForm(addressInfo);
        log.Information($"|{PAGE_NAME}| Signup form filled");
    }

    public void FillAccountInfoForm(AccountInfo accountInfo)
    {
        browser.FindElement(repo.GenderRadioBtn(accountInfo.Gender)).Click();
        browser.EnterText(repo.SignupNameField, accountInfo.Name);
        browser.EnterText(repo.SignupPasswordField, accountInfo.Password);
        browser.SelectFromDropDownByValue(repo.DayOfBirthDropDown, accountInfo.DateOfBirth.Day.ToString());
        browser.SelectFromDropDownByValue(repo.MonthOfBirthDropDown, accountInfo.DateOfBirth.Month.ToString());
        browser.SelectFromDropDownByValue(repo.YearOfBirthDropDown, accountInfo.DateOfBirth.Year.ToString());

        var newsletterCheckBox = browser.FindElement(repo.NewsletterCheckBox);
        if (newsletterCheckBox.Selected != accountInfo.IsNewsletter)
        {
            newsletterCheckBox.Click();
        }

        var specOfferCheckBox = browser.FindElement(repo.SpecOfferCheckBox);
        if (specOfferCheckBox.Selected != accountInfo.IsNewsletter)
        {
            specOfferCheckBox.Click();
        }

        log.Information($"|{PAGE_NAME}| Account information form filled");
    }

    public void FillAddressInfoForm(AddressInfo addressInfo)
    {
        browser.EnterText(repo.FirstNameField, addressInfo.FirstName);
        browser.EnterText(repo.LastNameField, addressInfo.LastName);
        browser.EnterText(repo.CompanyField, addressInfo.Company);
        browser.EnterText(repo.AddressField, addressInfo.Address);
        browser.EnterText(repo.Address2Field, addressInfo.Address2);

        browser.SelectFromDropDownByValue(repo.CountyDropDown, addressInfo.Country);

        browser.EnterText(repo.StateField, addressInfo.State);
        browser.EnterText(repo.CityField, addressInfo.City);
        browser.EnterText(repo.ZipcodeField, addressInfo.Zipcode.ToString());
        browser.EnterText(repo.MobileNumberField, addressInfo.MobileNumber.ToString());
        log.Information($"|{PAGE_NAME}| Address information form filled");
    }

    public void ClickOnCreateAccountBtn()
    {
        browser.FindElement(repo.CreateAccountBtn).Click();
        log.Information($"|{PAGE_NAME}| Clicked on 'Create Account' button");

    }

    public void ClickOnContinueBtn()
    {
        browser.FindElement(repo.ContinueBtn).Click();
        log.Information($"|{PAGE_NAME}| Clicked on 'Continue' button");

    }

    public string GetAccountCreatedMessage()
    {
        string createAccountMessage = browser.FindElement(repo.CreateAccountMessage).Text;
        log.Information($"|{PAGE_NAME}| Account created message: '{createAccountMessage}'");
        
        return createAccountMessage;
    }

    public string GetAccountDeletedMessage()
    {
        string deleteAccountMessage = browser.FindElement(repo.DeleteAccountMessage).Text;
        log.Information($"|{PAGE_NAME}| Account deleted message: '{deleteAccountMessage}'");
        
        return deleteAccountMessage;
    }
}
