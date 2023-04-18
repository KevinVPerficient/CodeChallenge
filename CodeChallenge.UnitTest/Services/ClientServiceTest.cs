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
    public class ClientServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IClientRepository> clientRepositoryMock = new Mock<IClientRepository>();
        private readonly ClientService clientService;
        private readonly List<Client> clients = FakeDataHelper.CreateFakeClient().Generate(4);
        public ClientServiceTest()
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

            clientService = new ClientService(clientRepositoryMock.Object, _mapper);
        }

        [Fact]
        public void GetAll_Should_Pass()
        {
            //Arrange
            clientRepositoryMock.Setup(x => x.GetAll())
                .Returns(clients);

            //Act
            var result = clientService.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(clients.Count());
            clientRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetByCity_Should_Pass()
        {
            //Arrange
            var City = "City";
            clientRepositoryMock.Setup(x => x.GetByCity(City))
                .ReturnsAsync(clients);

            //Act
            var result = await clientService.GetByCity(City);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(clients.Count());
            clientRepositoryMock.Verify(x => x.GetByCity(City), Times.Once);
        }

        [Fact]
        public async Task GetById_Should_Pass()
        {
            //Arrange
            var ClientDoc = "12345";
            clientRepositoryMock.Setup(x => x.GetById(ClientDoc))
                .ReturnsAsync(clients);

            //Act
            var result = await clientService.GetById(ClientDoc);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(clients.Count());
            clientRepositoryMock.Verify(x => x.GetById(ClientDoc), Times.Once);
        }

        [Fact]
        public async Task GetBySeller_Should_Pass()
        {
            //Arrange
            var SellerCode = "TEST";
            clientRepositoryMock.Setup(x => x.GetBySeller(SellerCode)).ReturnsAsync(clients); 

            //Act
            var result = await clientService.GetBySeller(SellerCode);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(clients.Count());
            clientRepositoryMock.Verify(x => x.GetBySeller(SellerCode), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Pass()
        {
            //Arrange
            clientRepositoryMock.Setup(x => x.GetById(clients.First().DocNumber))
                .ReturnsAsync(clients);
            var client = _mapper.Map<ClientDto>(clients.First());

            //Act
            await clientService.Update(client, client.DocNumber);

            //Assert
            clientRepositoryMock.Verify(x => x.GetById(clients.First().DocNumber), Times.Once);
            clientRepositoryMock.Verify(x => x.Update(clients.First()), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Thronw_Exception_When_Validation_Fail()
        {
            //Arrange
            clientRepositoryMock.Setup(x => x.GetById(clients.First().DocNumber))
                .ReturnsAsync(clients);
            var client = _mapper.Map<ClientDto>(clients.First());
            client.CellPhoneNumber = null;

            //Act
            var act = async () => { await clientService.Update(client, client.DocNumber); };

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Update_Should_Thronw_Exception_When_Client_Is_Not_Found()
        {
            //Arrange
            var emptyList = new List<Client>();
            clientRepositoryMock.Setup(x => x.GetById(clients.First().DocNumber))
                .ReturnsAsync(emptyList);
            var client = _mapper.Map<ClientDto>(clients.First());
            client.CellPhoneNumber = null;

            //Act
            var act = async () => { await clientService.Update(client, client.DocNumber); };

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Delete_Should_Pass()
        {
            //Arrange
            var Id = "12345678";
            clientRepositoryMock.Setup(x => x.GetById(Id))
                .ReturnsAsync(clients);

            //Act
            var result = await clientService.Delete(Id);

            //Assert
            result.Should().BeTrue();
            clientRepositoryMock.Verify(x => x.GetById(Id), Times.Once);
            clientRepositoryMock.Verify(x => x.Delete(clients.First()), Times.Once);
        }

        [Fact] 
        public async Task Delete_Should_Fail_When_Client_Is_Not_Found()
        {
            //Arrange
            var Id = "12345678";
            var emptyList = new List<Client>();
            clientRepositoryMock.Setup(x => x.GetById(Id))
                .ReturnsAsync(emptyList);

            //Act
            var result = await clientService.Delete(Id);

            //Assert
            result.Should().BeFalse();
            clientRepositoryMock.Verify(x => x.GetById(Id), Times.Once);
        }

        [Fact]
        public async Task Create_Should_Pass()
        {
            //Arrange
            clients.First().Branches = FakeDataHelper.CreateFakeBranch().Generate(2);
            var clientDto = _mapper.Map<ClientDto>(clients.First());

            //Act
            var result = await clientService.Create(clientDto);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Create_Should_Thronw_Exception_When_Validation_Fail()
        {
            //Arrange
            clients.First().Branches = FakeDataHelper.CreateFakeBranch().Generate(2);
            var clientDto = _mapper.Map<ClientDto>(clients.First());
            clientDto.CellPhoneNumber = null;

            //Act
            var act = async () => { await clientService.Create(clientDto); };

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Create_Should_Thronw_Exception_When_Client_Dont_Have_Branch()
        {
            //Arrange
            var clientDto = _mapper.Map<ClientDto>(clients.First());
            clientDto.Branches = null;

            //Act
            var act = async () => { await clientService.Create(clientDto); };

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
