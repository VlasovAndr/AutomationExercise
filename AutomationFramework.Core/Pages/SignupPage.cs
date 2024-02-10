using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;
using NUnit.Allure.Attributes;

namespace AutomationFramework.Core.Pages;

public class SignupPage : PageBase
{
    public Header Header => header;

    protected override string PageName => pageName;
    protected override string PageUrl => $"{BaseUrl}/signup";

    private const string pageName = "Signup Page";
    private readonly Header header;
    private readonly SignupLocators repo;

    public SignupPage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header, SignupLocators repo, ITestReporter reporter) 
        : base(browser, log, config, reporter)
    {
        this.header = header;
        this.repo = repo;
    }

    [AllureStep($"|{pageName}| Getting signup form title")]
    public string GetSignupFormTitle()
    {
        string formTitle = browser.FindElement(repo.SignupFormTitle).Text;
        LogParameterInfo("Signup form title", formTitle);

        return formTitle;
    }

    public void FillSignupForm(AccountInfo accountInfo, AddressInfo addressInfo)
    {
        FillAccountInfoForm(accountInfo);
        FillAddressInfoForm(addressInfo);
    }

    [AllureStep($"|{pageName}| Filling account info form")]
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
        if (specOfferCheckBox.Selected != accountInfo.IsSpecialOffers)
        {
            specOfferCheckBox.Click();
        }
    }

    [AllureStep($"|{pageName}| Filling address info form")]
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
    }

    [AllureStep($"|{pageName}| Submiting signup form")]
    public void SubmitSignupForm()
    {
        browser.FindElement(repo.CreateAccountBtn).Click();
    }

    [AllureStep($"|{pageName}| Clicking on continue button")]
    public void ClickOnContinueBtn()
    {
        browser.FindElement(repo.ContinueBtn).Click();
    }

    [AllureStep($"|{pageName}| Getting created message")]
    public string GetAccountCreatedMessage()
    {
        string createAccountMessage = browser.FindElement(repo.CreateAccountMessage).Text;
        LogParameterInfo("Account created message", createAccountMessage);

        return createAccountMessage;
    }

    [AllureStep($"|{pageName}| Getting deleted message")]
    public string GetAccountDeletedMessage()
    {
        string deleteAccountMessage = browser.FindElement(repo.DeleteAccountMessage).Text;
        LogParameterInfo("Account deleted message", deleteAccountMessage);

        return deleteAccountMessage;
    }
}
