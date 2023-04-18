using CodeChallenge.Data.Data;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services;
using CodeChallenge.Data.Services.Interfaces;
using CodeChallenge.UnitTest.Helper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.UnitTest.Repositories
{
    public class ClientRepositoryTest : IDisposable
    {
        private readonly ProjectDbContext context;
        private readonly IClientRepository clientRepository;
        private List<Client> clients = FakeDataHelper.CreateFakeClient().Generate(4);


        public ClientRepositoryTest()
        {
            foreach (var client in clients)
            {
                client.Branches = FakeDataHelper.CreateFakeBranch().Generate(1);
            }

            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb").Options;
            context = new ProjectDbContext(options);
            context.Clients.AddRange(clients);
            context.SaveChanges();

            clientRepository = new ClientRepository(context);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetAll_Should_Pass()
        {
            //Arrange
            var client = clients;

            //Act
            var result = clientRepository.GetAll();

            //Assert
            result.Should().NotBeNull(); 
            result.Should().HaveCount(clients.Count());
            result.Should().Contain(client);
        }

        [Fact]
        public async Task GetByCity_Should_Pass()
        {
            //Arrange
            var client = clients[0];
            
            //Act
            var result = await clientRepository.GetByCity(client.City);

            //Assert
            result.Should().NotBeNull();
            result.Should().Contain(client);
        }

        [Fact]
        public async Task GetById_Should_Pass()
        {
            //Arrange
            var client = clients[0];

            //Act
            var result = await clientRepository.GetById(client.DocNumber);

            //Assert
            result.Should().NotBeNull();
            result.Should().Contain(client);
        }

        [Fact]
        public async Task GetBySeller_Should_Pass()
        {
            //Arrange
            var client = clients[0];

            //Act
            var result = await clientRepository.GetBySeller(client.Branches.First().SellerCode);

            //Assert
            result.Should().NotBeNull();
            result.Should().Contain(client);
        }

        [Fact]
        public async Task Update_Should_Pass()
        {
            //Arrange
            var client = clients[0];
            client.FullName = "Kevin Vargas";

            //Act
            var act = async () => { await clientRepository.Update(client); };
            var newClients = context.Clients.ToList();

            //Assert
            await act.Should().NotThrowAsync<Exception>();
            newClients.Should().Contain(client);
        }

        [Fact]
        public async Task Delete_Should_Pass()
        {
            //Arrange
            var client = clients[0];

            //Act
            var act = async () => { await clientRepository.Delete(client); };

            //Assert
            await act.Should().NotThrowAsync<Exception>();
            var newClients = context.Clients.ToList();
            newClients.Should().NotContain(client);
        }

        [Fact]
        public async Task Create_Should_Pass()
        {
            //Arrange
            var newClient = (FakeDataHelper.CreateFakeClient().Generate(1)).First();

            //Act
            var result = await clientRepository.Create(newClient);
            var allClients = context.Clients.ToList();

            //Assert
            result.Should().Be(newClient);
            allClients.Should().Contain(newClient);
        }
    }
}
