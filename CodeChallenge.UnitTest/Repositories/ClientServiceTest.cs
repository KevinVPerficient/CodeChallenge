using AutoMapper;
using CodeChallenge.Business.Services;
using CodeChallenge.Data.Services.Interfaces;
using CodeChallenge.UnitTest.Helper;
using FluentAssertions;
using Moq;

namespace CodeChallenge.UnitTest.Services
{
    public class ClientServiceTest
    {
        [Fact]
        public void GetAll_Should_Pass()
        {
            //Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var mapperMock = new Mock<IMapper>();
            var clientService = new ClientService(clientRepositoryMock.Object, mapperMock.Object);
            var clients = FakeDataHelper.CreateFakeClientDto().Generate(2);
            clientRepositoryMock.Setup(x => x.GetAll()).Returns(clients);

            //Act
            var result = clientService.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(clients.Count);
        }
    }
}
