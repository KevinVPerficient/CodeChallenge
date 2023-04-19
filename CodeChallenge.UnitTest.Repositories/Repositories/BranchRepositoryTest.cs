using CodeChallenge.Data.Data;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services;
using CodeChallenge.Data.Services.Interfaces;
using CodeChallenge.UnitTest.Helper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.UnitTest.Repositories
{
    public class BranchRepositoryTest : IDisposable
    {
        private readonly ProjectDbContext context;
        private readonly IBranchRepository branchRepository;
        private IReadOnlyList<Client> clients = FakeDataHelper.CreateFakeClient().Generate(4);
        private List<Branch> branches = FakeDataHelper.CreateFakeBranch().Generate(1);

        public BranchRepositoryTest()
        {
            for (var i = 0; i < clients.Count(); i++)
            {
                clients[i].Branches = branches;
            }

            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb").Options;
            context = new ProjectDbContext(options);
            context.AddRange(clients);
            context.SaveChanges();

            branchRepository = new BranchRepository(context);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetAll_Should_Pass()
        {
            //Act
            var result = branchRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(branches.Count());
        }

        [Fact]
        public async Task GetByCity_Should_Pass()
        {
            //Arrange
            var branch = branches[0];

            //Act
            var result = await branchRepository.GetByCity(branch.City);

            //Assert
            result.Should().NotBeNull();
            result.Should().Contain(branch);
        }

        [Fact]
        public async Task GetByCity_Should_Return_Empty_List()
        {
            //Arrange
            var fakeCity = "FakeCity";

            //Act
            var result = await branchRepository.GetByCity(fakeCity);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetById_Should_Pass()
        {
            //Arrange
            var branch = branches[0];

            //Act
            var result = await branchRepository.GetById(branch.Code);

            //Assert
            Assert.Single(result);
            result.Should().Contain(branch);
        }

        [Fact]
        public async Task GetById_Should_Return_Empty_List()
        {
            //Arrange
            var fakeId = "FakeId";

            //Act
            var result = await branchRepository.GetById(fakeId);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByClientDocument_Should_Pass()
        {
            //Arrange
            var client = clients[0];

            //Act
            var result = await branchRepository.GetByClientDocument(client.DocNumber);

            //Assert
            result.Should().NotBeNull();
            result.Should().Contain(client.Branches);
        }

        [Fact]
        public async Task GetByClientDocument_Should_Fail_When_Client_Dont_Exist()
        {
            //Arrange
            var fakeDoc = "FakeDocument";

            //Act
            var act = async () => { await branchRepository.GetByClientDocument(fakeDoc); };

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Update_Should_Pass()
        {
            //Arrange
            var branch = branches[0];
            branch.Email = "new_mail@mail.com";

            //Act
            var act = async () => { await branchRepository.Update(branch); };
            var newBranch = context.Branches.ToList();

            //Assert
            await act.Should().NotThrowAsync<Exception>();
            newBranch.Should().Contain(branch);
        }

        [Fact]
        public async Task Delete_Should_Pass()
        {
            //Arrange
            var branch = branches[0];

            //Act
            var act = async () => { await branchRepository.Delete(branch); };

            //Assert
            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task Create_Should_Pass()
        {
            // Arrange
            var branch = (FakeDataHelper.CreateFakeBranch().Generate(1)).First();

            // Act
            var result = await branchRepository.Create(branch);

            // Assert
            Assert.NotNull(result);
            var savedBranch = await context.Branches.FindAsync(result.BranchId);
            Assert.NotNull(savedBranch);
        }
    }
}
