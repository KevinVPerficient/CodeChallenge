using Bogus;
using CodeChallenge.Data.Enums;
using CodeChallenge.Data.Models;

namespace CodeChallenge.UnitTest.Helper
{
    internal class FakeDataHelper
    {
        public static Faker<Client> CreateFakeClientDto()
            => new Faker<Client>()
            .RuleFor(x => x.ClientType, ClientType.PersonaNatural)
            .RuleFor(x => x.FullName, fake => fake.Name.FullName())
            .RuleFor(x => x.DocType, DocType.Cedula)
            .RuleFor(x => x.DocNumber, fake => fake.Random.AlphaNumeric(8))
            .RuleFor(x => x.Address, fake => fake.Address.FullAddress())
            .RuleFor(x => x.City, fake => fake.Address.City())
            .RuleFor(x => x.Country, fake => fake.Address.Country())
            .RuleFor(x => x.CellPhoneNumber, fake => fake.Phone.PhoneNumber())
            .RuleFor(x => x.Email, fake => fake.Internet.Email());
    }
}
