﻿using AutomationFramework.Common.Abstractions;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;

namespace AutomationFramework.Core.Pages;

public class SignupAndLoginPage : PageBase
{
    private const string PAGE_NAME = "Signup / Login Page";
    private string PageUrl => $"{BaseUrl}/login";

    private readonly Header header;
    private readonly SignupAndLoginLocators repo;

    public Header Header => header;

    public SignupAndLoginPage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config, Header header, SignupAndLoginLocators repo)
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

    public void FillSignupForm(string name, string email)
    {
        browser.EnterText(repo.SignupNameField, name);
        browser.EnterText(repo.SignupEmailField, email);
        log.Information($"|{PAGE_NAME}| Signup form filled");
    }

    public void ClickOnSignUpBtn()
    {
        browser.FindElement(repo.SignupBtn).Click();
        log.Information($"|{PAGE_NAME}| Clicked on 'SignUp' button");
    }

    public string GetLoginFormTitle()
    {
        string formTitle = browser.FindElement(repo.LoginFormTitle).Text;
        log.Information($"|{PAGE_NAME}| Login form title: '{formTitle}'");
        
        return formTitle;
    }

    public void FillLoginForm(string email, string password)
    {
        browser.EnterText(repo.LoginEmailField, email);
        browser.EnterText(repo.LoginPasswordField, password);
        log.Information($"|{PAGE_NAME}| Login form filled");
    }

    public void ClickOnLoginBtn()
    {
        browser.FindElement(repo.LoginBtn).Click();
        log.Information($"|{PAGE_NAME}| Clicked on 'Login' button");
    }
}
