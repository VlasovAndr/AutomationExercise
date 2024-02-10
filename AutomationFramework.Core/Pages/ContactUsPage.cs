using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Common.Variables;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace AutomationFramework.Core.Pages;

public class ContactUsPage : PageBase
{
    public Header Header => header;

    protected override string PageName => pageName;
    protected override string PageUrl => $"{BaseUrl}/contact_us";

    private readonly Header header;
    private readonly ContactUsLocators repo;
    private readonly DefaultVariables variables;
    private const string pageName = "ContactUsPage Page";

    public ContactUsPage(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config,
        Header header, ContactUsLocators repo, DefaultVariables variables, ITestReporter reporter)
        : base(browser, log, config, reporter)
    {
        this.header = header;
        this.repo = repo;
        this.variables = variables;
    }

    [AllureStep($"|{pageName}| Getting contact us form title")]
    public string GetContactUsFormTitle()
    {
        string formTitle = browser.FindElement(repo.ContactUsFormHeader).Text;
        LogParameterInfo("Contact us form title", formTitle);

        return formTitle;
    }

    [AllureStep($"|{pageName}| Filling contact us form")]
    public void FillContactUsForm(ContactUsInfo data)
    {
        browser.EnterText(repo.NameField, data.Name);
        browser.EnterText(repo.EmailField, data.Email);
        browser.EnterText(repo.SubjectField, data.Subject);
        browser.EnterText(repo.MessageField, data.Message);
    }

    [AllureStep($"|{pageName}| Uploading file")]
    public void UploadFile(string fileName)
    {
        var fullFilePath = Path.Combine(variables.UITestDataFolder, fileName);
        browser.EnterText(repo.UploadFileField, fullFilePath);
    }

    [AllureStep($"|{pageName}| Submiting contact us form")]
    public void Submit()
    {
        browser.FindElement(repo.SubmitBtn).Click();
        IAlert alert = browser.WebDriver.SwitchTo().Alert();
        alert.Accept();
    }

    [AllureStep($"|{pageName}| Getting successful message")]
    public string GetSuccessfulMessage()
    {
        string message = browser.FindElement(repo.SuccessfulMessage).Text;
        LogParameterInfo("Successful message", message);

        return message;
    }
}

