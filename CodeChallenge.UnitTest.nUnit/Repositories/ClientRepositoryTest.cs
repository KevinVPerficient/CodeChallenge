﻿using CodeChallenge.Data.Data;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services;
using CodeChallenge.Data.Services.Interfaces;
using CodeChallenge.UnitTest.nUnit.Helper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.UnitTest.nUnit.Repositories
{
    public class ClientRepositoryTest : IDisposable
    {
        private readonly ProjectDbContext context;
        private readonly IClientRepository clientRepository;
        private IReadOnlyList<Client> clients = FakeDataHelper.CreateFakeClient().Generate(4);

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

        [Test]
        public void GetAll_Should_Pass()
        {
            //Arrange
            var clientCount = clients.Count();

            //Act
            var result = clientRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(clientCount, result.Count());
        }

        [Test]
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

        [Test]
        public async Task GetByCity_Should_Return_Empty_List()
        {
            //Arrange
            var fakeCity = "Fake City";

            //Act
            var result = await clientRepository.GetByCity(fakeCity);

            //Assert
            result.Should().BeEmpty();
        }

        [Test]
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

        [Test]
        public async Task GetById_Should_Return_Empty_List()
        {
            //Arrange
            var fakeId = "FakeId";

            //Act
            var result = await clientRepository.GetById(fakeId);

            //Assert
            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetBySeller_Should_Pass()
        {
            // Arrange
            var sellerCode = clients[0].Branches.First().SellerCode;

            // Act
            var result = await clientRepository.GetBySeller(sellerCode);

            // Assert
            result.Should().Contain(clients[0]);
        }

        [Test]
        public async Task GetBySeller_Should_Return_Empty_List()
        {
            //Arrange
            var fakeSeller = "FakeSeller";

            //Act
            var result = await clientRepository.GetBySeller(fakeSeller);

            //Assert
            result.Should().BeEmpty();
        }

        [Test]
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

        [Test]
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

        [Test]
        public async Task Create_Should_Pass()
        {
            // Arrange
            var client = (FakeDataHelper.CreateFakeClient().Generate(1)).First();
            client.Branches = FakeDataHelper.CreateFakeBranch().Generate(1);

            // Act
            var result = await clientRepository.Create(client);

            // Assert
            Assert.NotNull(result);
            var savedClient = await context.Clients.FindAsync(result.ClientId);
            Assert.NotNull(savedClient);
        }
    }
}
