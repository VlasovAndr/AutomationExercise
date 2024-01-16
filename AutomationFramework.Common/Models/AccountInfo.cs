namespace AutomationFramework.Common.Models;

public class AccountInfo
{
    public string Gender { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsNewsletter { get; set; }
    public bool IsSpecialOffers { get; set; }

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
