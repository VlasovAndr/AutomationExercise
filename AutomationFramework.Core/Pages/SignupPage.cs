﻿using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;

namespace AutomationFramework.Core.Pages;

public class SignupPage : PageBase
{
    private const string PAGE_NAME = "Login Page";
    private string PageUrl => $"{BaseUrl}/signup";

    public readonly Header header;
    private readonly SignupLocators repo;

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

    public string GetLoginFormTitle()
    {
        return browser.FindElement(repo.LoginFormTitle).Text;
    }

    public void FillLoginForm(User user)
    {
        FillAccountInfoForm(user.Account);
        FillAddressInfoForm(user.Address);
    }

    public void FillAccountInfoForm(AccountInfo accountInfo)
    {
        browser.FindElement(repo.GenderRadioBtn(accountInfo.Gender)).Click();
        browser.EnterText(repo.LoginNameField, accountInfo.Name);
        browser.EnterText(repo.LoginEmailField, accountInfo.Email);
        browser.EnterText(repo.LoginPasswordField, accountInfo.Password);
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
    }

    public void ClickOnCreateAccountBtn()
    {
        browser.FindElement(repo.CreateAccountBtn).Click();
    }

    public string GetAccountCreatedMessage()
    {
        return browser.FindElement(repo.CreateAccountMessage).Text;
    }

    public string GetAccountDeletedMessage()
    {
        return browser.FindElement(repo.DeleteAccountMessage).Text;
    }

    public string ClickOnContinueBtn()
    {
        return browser.FindElement(repo.ContinueBtn).Text;
    }
}