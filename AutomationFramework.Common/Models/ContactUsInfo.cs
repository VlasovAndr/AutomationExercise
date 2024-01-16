namespace AutomationFramework.Common.Models;

public class ContactUsInfo
{

    public ContactUsInfo(string name, string email, string subject, string message)
    {
        Name = name;
        Email = email;
        Subject = subject;
        Message = message;
    }

    public string Name { get; }
    public string Email { get; }
    public string Subject { get; }
    public string Message { get; }
}
