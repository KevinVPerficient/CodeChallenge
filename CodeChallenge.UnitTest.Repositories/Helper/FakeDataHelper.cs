using Bogus;
using CodeChallenge.Data.Enums;
using CodeChallenge.Data.Models;

namespace CodeChallenge.UnitTest.Helper
{
    internal class FakeDataHelper
    {
        public static Faker<Client> CreateFakeClient()
            => new Faker<Client>()
            .RuleFor(x => x.ClientType, fake => ClientType.PersonaNatural)
            .RuleFor(x => x.FullName, fake => fake.Name.FullName())
            .RuleFor(x => x.DocType, fake => DocType.Cedula)
            .RuleFor(x => x.DocNumber, fake => fake.Random.AlphaNumeric(8))
            .RuleFor(x => x.Address, fake => fake.Address.FullAddress())
            .RuleFor(x => x.City, fake => fake.Address.City())
            .RuleFor(x => x.Country, fake => fake.Address.Country())
            .RuleFor(x => x.CellPhoneNumber, fake => fake.Phone.PhoneNumber("##########"))
            .RuleFor(x => x.Email, fake => fake.Internet.Email());


        public static Faker<Branch> CreateFakeBranch()
            => new Faker<Branch>()
            .RuleFor(x => x.Code, fake => fake.Random.AlphaNumeric(5))
            .RuleFor(x => x.Name, fake => fake.Company.CompanyName())
            .RuleFor(x => x.SellerCode, fake => fake.Random.AlphaNumeric(5))
            .RuleFor(x => x.Credit, fake => int.Parse(fake.Commerce.Price(1000, 10000, 0)))
            .RuleFor(x => x.Address, fake => fake.Address.FullAddress())
            .RuleFor(x => x.City, fake => fake.Address.City())
            .RuleFor(x => x.Country, fake => fake.Address.Country())
            .RuleFor(x => x.CellPhoneNumber, fake => fake.Phone.PhoneNumber("##########"))
            .RuleFor(x => x.Email, fake => fake.Internet.Email());
    }
}
