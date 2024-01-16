using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Common.Variables;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;
using OpenQA.Selenium;

namespace AutomationFramework.Core.Pages;

public class ContactUsPage : PageBase
{
    public Header Header => header;

    protected override string PageName => "ContactUsPage Page";
    protected override string PageUrl => $"{BaseUrl}/contact_us";

    private readonly Header header;
    private readonly ContactUsLocators repo;
    private readonly DefaultVariables variables;

    public ContactUsPage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config,
        Header header, ContactUsLocators repo, DefaultVariables variables)
        : base(browser, log, config)
    {
        this.header = header;
        this.repo = repo;
        this.variables = variables;
    }

    public string GetContactUsFormTitle()
    {
        string formTitle = browser.FindElement(repo.ContactUsFormHeader).Text;
        LogPageInfo($"Contact form header: '{formTitle}'");

        return formTitle;
    }

    public void FillContactUsForm(ContactUsInfo data)
    {
        browser.EnterText(repo.NameField, data.Name);
        browser.EnterText(repo.EmailField, data.Email);
        browser.EnterText(repo.SubjectField, data.Subject);
        browser.EnterText(repo.MessageField, data.Message);
        LogPageInfo($"Contact us form filled");

    }

    public void UploadFile(string filePath)
    {
        var fullFilePath = Path.Combine(variables.UITestDataFolder, filePath);
        browser.EnterText(repo.UploadFileField, fullFilePath);
        LogPageInfo($"Contact us file uploaded");
    }

    public void Submit()
    {
        browser.FindElement(repo.SubmitBtn).Click();
        IAlert alert = browser.WebDriver.SwitchTo().Alert();
        alert.Accept();
        LogPageInfo($"Contact us form submited");
    }

    public string GetSuccessfulMessage()
    {
        string message = browser.FindElement(repo.SuccessfulMessage).Text;
        LogPageInfo($"Successful message: '{message}'");

        return message;
    }
}

