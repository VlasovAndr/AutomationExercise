using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Models;
using AutomationFramework.Common.Variables;
using AutomationFramework.Core.Configuration;
using AutomationFramework.Core.Pages.Locators;
using Microsoft.Playwright;
using NUnit.Allure.Attributes;

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

    public ContactUsPage(IPage page, TestRunConfiguration config, Header header, 
        ContactUsLocators repo, DefaultVariables variables, ITestReporter reporter)
        : base(page, config, reporter)
    {
        this.header = header;
        this.repo = repo;
        this.variables = variables;
    }

    [AllureStep($"|{pageName}| Getting contact us form title")]
    public async Task<string> GetContactUsFormTitle()
    {
        var formTitle = await Page.Locator(repo.ContactUsFormHeader).TextContentAsync();
        LogParameterInfo("Contact us form title", formTitle);

        return formTitle;
    }

    [AllureStep($"|{pageName}| Filling contact us form")]
    public async Task FillContactUsForm(ContactUsInfo data)
    {
        await Page.Locator(repo.NameField).FillAsync(data.Name);
        await Page.Locator(repo.EmailField).FillAsync(data.Email);
        await Page.Locator(repo.SubjectField).FillAsync(data.Subject);
        await Page.Locator(repo.MessageField).FillAsync(data.Message);
    }

    [AllureStep($"|{pageName}| Uploading file")]
    public async Task UploadFile(string fileName)
    {
        var fullFilePath = Path.Combine(variables.UITestDataFolder, fileName);
        await Page.Locator(repo.UploadFileField).SetInputFilesAsync(fullFilePath);
    }

    [AllureStep($"|{pageName}| Submiting contact us form")]
    public async Task Submit()
    {
        Thread.Sleep(1000);
        Page.Dialog += async (_, dialog) =>
        {
            await dialog.AcceptAsync();
        };
        await Page.Locator(repo.SubmitBtn).ClickAsync();
    }

    [AllureStep($"|{pageName}| Getting successful message")]
    public async Task<string> GetSuccessfulMessage()
    {
        var message = await Page.Locator(repo.SuccessfulMessage).TextContentAsync();

        LogParameterInfo("Successful message", message);

        return message;
    }
}

