using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;
using Microsoft.Playwright;
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

    public SignupPage(IPage page, TestRunConfiguration config, Header header, SignupLocators repo, ITestReporter reporter)
        : base(page, config, reporter)
    {
        this.header = header;
        this.repo = repo;
    }

    [AllureStep($"|{pageName}| Getting signup form title")]
    public async Task<string> GetSignupFormTitle()
    {
        var formTitle = await Page.Locator(repo.SignupFormTitle).TextContentAsync();

        LogParameterInfo("Signup form title", formTitle);

        return formTitle;
    }

    public async Task FillSignupForm(AccountInfo accountInfo, AddressInfo addressInfo)
    {
        FillAccountInfoForm(accountInfo);
        FillAddressInfoForm(addressInfo);
    }

    [AllureStep($"|{pageName}| Filling account info form")]
    public async Task FillAccountInfoForm(AccountInfo accountInfo)
    {
        await Page.Locator(repo.GenderRadioBtn(accountInfo.Gender)).ClickAsync();
        await Page.Locator(repo.SignupNameField).FillAsync(accountInfo.Name);
        await Page.Locator(repo.SignupPasswordField).FillAsync(accountInfo.Password);

        await Page.Locator(repo.DayOfBirthDropDown).SelectOptionAsync(accountInfo.DateOfBirth.Day.ToString());
        await Page.Locator(repo.MonthOfBirthDropDown).SelectOptionAsync(accountInfo.DateOfBirth.Month.ToString());
        await Page.Locator(repo.YearOfBirthDropDown).SelectOptionAsync(accountInfo.DateOfBirth.Year.ToString());

        var newsletterCheckBox = Page.Locator(repo.NewsletterCheckBox);
        if (await newsletterCheckBox.IsCheckedAsync() != accountInfo.IsNewsletter)
        {
            await newsletterCheckBox.ClickAsync();
        }

        var specOfferCheckBox = Page.Locator(repo.SpecOfferCheckBox);
        if (await specOfferCheckBox.IsCheckedAsync() != accountInfo.IsSpecialOffers)
        {
            await specOfferCheckBox.ClickAsync();
        }
    }

    [AllureStep($"|{pageName}| Filling address info form")]
    public async Task FillAddressInfoForm(AddressInfo addressInfo)
    {
        await Page.Locator(repo.FirstNameField).FillAsync(addressInfo.FirstName);
        await Page.Locator(repo.LastNameField).FillAsync(addressInfo.LastName);
        await Page.Locator(repo.CompanyField).FillAsync(addressInfo.Company);
        await Page.Locator(repo.AddressField).FillAsync(addressInfo.Address);
        await Page.Locator(repo.Address2Field).FillAsync(addressInfo.Address2);

        await Page.Locator(repo.CountyDropDown).SelectOptionAsync(addressInfo.Country);

        await Page.Locator(repo.StateField).FillAsync(addressInfo.State);
        await Page.Locator(repo.CityField).FillAsync(addressInfo.City);
        await Page.Locator(repo.ZipcodeField).FillAsync(addressInfo.Zipcode.ToString());
        await Page.Locator(repo.MobileNumberField).FillAsync(addressInfo.MobileNumber.ToString());
    }

    [AllureStep($"|{pageName}| Submiting signup form")]
    public async Task SubmitSignupForm()
    {
        await Page.Locator(repo.CreateAccountBtn).ClickAsync();

    }

    [AllureStep($"|{pageName}| Clicking on continue button")]
    public async Task ClickOnContinueBtn()
    {
        await Page.Locator(repo.ContinueBtn).ClickAsync();
    }

    [AllureStep($"|{pageName}| Getting created message")]
    public async Task<string> GetAccountCreatedMessage()
    {
        var createAccountMessage = await Page.Locator(repo.CreateAccountMessage).TextContentAsync();

        LogParameterInfo("Account created message", createAccountMessage);

        return createAccountMessage;
    }

    [AllureStep($"|{pageName}| Getting deleted message")]
    public async Task<string> GetAccountDeletedMessage()
    {
        var deleteAccountMessage = await Page.Locator(repo.DeleteAccountMessage).TextContentAsync();

        LogParameterInfo("Account deleted message", deleteAccountMessage);

        return deleteAccountMessage;
    }
}
