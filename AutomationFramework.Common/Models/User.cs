namespace AutomationFramework.Common.Models;

public class User
{
    public AccountInfo Account { get; }
    public AddressInfo Address { get; }

    public User(AccountInfo accountInfo, AddressInfo addressInfo)
    {
        Account = accountInfo;
        Address = addressInfo;
    }
}
