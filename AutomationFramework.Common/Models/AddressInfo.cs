namespace AutomationFramework.Common.Models;

public class AddressInfo
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Company { get; }
    public string Address { get; }
    public string Address2 { get; }
    public string Country { get; }
    public string State { get; }
    public string City { get; }
    public string Zipcode { get; }
    public string MobileNumber { get; }

    public AddressInfo(string firstName, string lastName, string company, string address,
        string address2, string country, string state, string city, string zipcode, string mobileNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Company = company;
        Address = address;
        Address2 = address2;
        Country = country;
        State = state;
        City = city;
        Zipcode = zipcode;
        MobileNumber = mobileNumber;
    }
}
