using CodeChallenge.Data.DTOs;

namespace CodeChallenge.Business.Services.Interfaces
{
    public interface IClientService : IRepositoryService<ClientDto>
    {
        public Task<IEnumerable<ClientDto>> GetBySeller(string City);
    }
}
