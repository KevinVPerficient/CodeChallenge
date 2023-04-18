using AutoMapper;
using CodeChallenge.Business.DTOs.Mapping;
using CodeChallenge.Business.Services;
using CodeChallenge.Data.DTOs;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services.Interfaces;
using CodeChallenge.UnitTest.Helper;
using FluentAssertions;
using Moq;

namespace CodeChallenge.UnitTest.Services
{
    public class BranchServiceTest
    {
        private static IMapper _mapper;
        private Mock<IBranchRepository> branchRepositoryMock = new Mock<IBranchRepository>();
        private Mock<IClientRepository> clientRepositoryMock = new Mock<IClientRepository>();
        private readonly BranchService branchService;
        private readonly List<Branch> branches = FakeDataHelper.CreateFakeBranch().Generate(2);

        public BranchServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            branchService = new BranchService(branchRepositoryMock.Object, _mapper, clientRepositoryMock.Object);
        }

        [Fact]
        public void GetAll_Should_Pass()
        {
            //Arrange
            branchRepositoryMock.Setup(x => x.GetAll())
                .Returns(branches);

            //Act
            var result = branchService.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(branches.Count());
            branchRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetByCity_Should_Pass()
        {
            //Arrange
            var City = "City";
            branchRepositoryMock.Setup(x => x.GetByCity(City))
                .ReturnsAsync(branches);

            //Act
            var result = await branchService.GetByCity(City);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(branches.Count());
            branchRepositoryMock.Verify(x => x.GetByCity(City), Times.Once);
        }

        [Fact]
        public async Task GetById_Should_Pass()
        {
            //Arrange
            var BranchId = "12345";
            branchRepositoryMock.Setup(x => x.GetById(BranchId))
                .ReturnsAsync(branches);

            //Act
            var result = await branchService.GetById(BranchId);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(branches.Count());
            branchRepositoryMock.Verify(x => x.GetById(BranchId), Times.Once);
        }

        [Fact]
        public async Task GetByClientDoc_Should_Pass()
        {
            //Arrange
            var ClientDoc = "12345";
            branchRepositoryMock.Setup(x => x.GetByClientDocument(ClientDoc))
                .ReturnsAsync(branches);

            //Act
            var result = await branchService.GetByClientDoc(ClientDoc);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(branches.Count());
            branchRepositoryMock.Verify(x => x.GetByClientDocument(ClientDoc), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Pass()
        {
            //Arrange
            var client = FakeDataHelper.CreateFakeClient().Generate(1);

            branches.First().Client = client.First();
            branchRepositoryMock.Setup(x => x.GetById(branches.First().Code))
                .ReturnsAsync(branches);
            var branch = _mapper.Map<BranchDto>(branches.First());

            //Act
            await branchService.Update(branch, branch.Code, client.First().DocNumber);

            //Assert
            branchRepositoryMock.Verify(x => x.GetById(branches.First().Code), Times.Once);
            branchRepositoryMock.Verify(x => x.Update(branches.First()), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Thrown_Exception_When_Validation_Fail()
        {
            //Arrange
            var client = FakeDataHelper.CreateFakeClient().Generate(1);

            branches.First().Client = client.First();
            branchRepositoryMock.Setup(x => x.GetById(branches.First().Code))
                .ReturnsAsync(branches);
            var branch = _mapper.Map<BranchDto>(branches.First());
            branch.CellPhoneNumber = null;

            //Act
            var act = async () => { await branchService.Update(branch, branch.Code, client.First().DocNumber); };

            //Assert
            var ex = await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Update_Should_Thrown_Exception_When_Branch_Is_Not_Found()
        {
            //Arrange
            var emptyList = new List<Branch>();
            var client = FakeDataHelper.CreateFakeClient().Generate(1);
            branches.First().Client = client.First();
            branchRepositoryMock.Setup(x => x.GetById(branches.First().Code))
                .ReturnsAsync(emptyList);
            var branch = _mapper.Map<BranchDto>(branches.First());

            //Act
            var act = async () => { await branchService.Update(branch, branch.Code, client.First().DocNumber); };

            //Assert
            var ex = await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Delete_Should_Pass()
        {
            //Arrange
            var Id = "12345678";
            var client = FakeDataHelper.CreateFakeClient().Generate(1);
            branches.First().Client = client.First();
            branchRepositoryMock.Setup(x => x.GetById(Id))
                .ReturnsAsync(branches);

            //Act
            var result = await branchService.Delete(Id, client.First().DocNumber);

            //Assert
            result.Should().BeTrue();
            branchRepositoryMock.Verify(x => x.GetById(Id), Times.Once);
            branchRepositoryMock.Verify(x => x.Delete(branches.First()), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Fail_When_Branch_Is_Not_Found()
        {
            //Arrange
            var emptyList = new List<Branch>();
            var Id = "12345678";
            var client = FakeDataHelper.CreateFakeClient().Generate(1);
            branches.First().Client = client.First();
            branchRepositoryMock.Setup(x => x.GetById(Id))
                .ReturnsAsync(emptyList);

            //Act
            var result = await branchService.Delete(Id, client.First().DocNumber);

            //Assert
            result.Should().BeFalse();
            branchRepositoryMock.Verify(x => x.GetById(Id), Times.Once);
        }

        [Fact]
        public async Task Create_Should_Pass()
        {
            //Arrange
            var ClientDoc = "123450";
            var client = FakeDataHelper.CreateFakeClient().Generate(1);
            var branchDto = _mapper.Map<BranchDto>(branches.First());
            clientRepositoryMock.Setup(x => x.GetById(ClientDoc))
                .ReturnsAsync(client);
            //Act
            var result = await branchService.Create(branchDto, ClientDoc);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Create_Should_Thrown_Exception_When_Validation_Fail()
        {
            //Arrange
            var ClientDoc = "123450";
            var client = FakeDataHelper.CreateFakeClient().Generate(1);
            var branchDto = _mapper.Map<BranchDto>(branches.First());
            clientRepositoryMock.Setup(x => x.GetById(ClientDoc))
                .ReturnsAsync(client);
            branchDto.CellPhoneNumber = null;

            //Act
            var act = async () => { await branchService.Create(branchDto, ClientDoc); };

            //Assert
            var ex = await act.Should().ThrowAsync<Exception>(); 
        }

        [Fact]
        public async Task Create_Should_Thrown_Exception_When_client_Is_Not_Found()
        {
            //Arrange
            var ClientDoc = "123450";
            var emptyList = new List<Client>();
            var branchDto = _mapper.Map<BranchDto>(branches.First());
            clientRepositoryMock.Setup(x => x.GetById(ClientDoc))
                .ReturnsAsync(emptyList);
            branchDto.CellPhoneNumber = null;

            //Act
            var act = async () => { await branchService.Create(branchDto, ClientDoc); };

            //Assert
            var ex = await act.Should().ThrowAsync<Exception>();
        }
    }
}
