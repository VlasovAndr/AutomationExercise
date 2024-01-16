namespace AutomationFramework.Common.Models;

public class User
{
    public AccountInfo Account { get; set; }
    public AddressInfo Address { get; set; }

    public User(AccountInfo accountInfo, AddressInfo addressInfo)
    {
        Account = accountInfo;
        Address = addressInfo;
    }
}
