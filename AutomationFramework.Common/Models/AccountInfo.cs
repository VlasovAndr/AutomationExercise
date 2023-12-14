namespace AutomationFramework.Common.Models;

public class AccountInfo
{
    public string Gender { get; }
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }
    public DateTime DateOfBirth { get; }
    public bool IsNewsletter { get; }
    public bool IsSpecialOffers { get; }

    public AccountInfo(string gender, string name, string email, string password, DateTime dateOfBirth, bool newsletter, bool specialOffers)
    {
        Gender = gender;
        Name = name;
        Email = email;
        Password = password;
        DateOfBirth = dateOfBirth;
        IsNewsletter = newsletter;
        IsSpecialOffers = specialOffers;
    }
}
