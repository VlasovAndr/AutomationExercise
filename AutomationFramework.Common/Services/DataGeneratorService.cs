using AutomationFramework.Common.Models;
using Bogus;

namespace AutomationFramework.Common.Services;

public class DataGeneratorService
{
    private readonly Faker _faker;

    public DataGeneratorService()
    {
        _faker = new Faker();
    }

    public User GenerateRandomUser(bool? newsletterInput = null, bool? specialOffersInput = null)
    {
        var accountInfo = new AccountInfo(
            gender: _faker.PickRandom<Gender>().ToString(),
            name: _faker.Name.FirstName(),
            email: _faker.Internet.Email(),
            password: _faker.Internet.Password(),
            dateOfBirth: _faker.Person.DateOfBirth,
            newsletter: newsletterInput == null ? _faker.Random.Bool() : (bool)newsletterInput,
            specialOffers: specialOffersInput == null ? _faker.Random.Bool() : (bool)specialOffersInput
            );

        var addressInfo = new AddressInfo(
            firstName: _faker.Person.FirstName,
            lastName: _faker.Person.LastName,
            company: _faker.Company.CompanyName(),
            address: _faker.Address.FullAddress(),
            address2: _faker.Address.FullAddress(),
            country: _faker.PickRandom<Country>().ToString().Replace("_", " "),
            state: _faker.Address.State(),
            city: _faker.Address.City(),
            zipcode: _faker.Address.ZipCode(),
            mobileNumber: _faker.Person.Phone
            );

        return new User(accountInfo, addressInfo);
    }

    public ContactUsInfo GenerateContactUsInfo()
    {
        var contactUsInfo = new ContactUsInfo(
            _faker.Name.FirstName(),
            _faker.Internet.Email(),
            _faker.Commerce.Random.String2(20),
            _faker.Commerce.Random.String2(20));

        return contactUsInfo;
    }
}
