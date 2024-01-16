namespace AutomationFramework.Core.Pages.Locators;

public class ContactUsLocators
{
    public string ContactUsForm => $".//div[@class = 'contact-form']";
    public string ContactUsFormHeader => $"{ContactUsForm}/h2";

    public string NameField => $".//input[@data-qa= 'name']";
    public string EmailField => $".//input[@data-qa= 'email']";
    public string SubjectField => $".//input[@data-qa= 'subject']";
    public string MessageField => $".//textarea[@data-qa= 'message']";
    public string UploadFileField => $".//input[@name= 'upload_file']";
    public string SubmitBtn => $".//input[@data-qa= 'submit-button']";
    public string SuccessfulMessage => $".//div[contains(@class, 'status alert')]";
}
