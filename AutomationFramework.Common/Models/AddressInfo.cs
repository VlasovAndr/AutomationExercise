namespace AutomationFramework.Common.Models;

public class AddressInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Zipcode { get; set; }
    public string MobileNumber { get; set; }

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
