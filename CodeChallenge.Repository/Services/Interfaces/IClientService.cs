using CodeChallenge.Data.DTOs;

namespace CodeChallenge.Business.Services.Interfaces
{
    public interface IClientService : IRepositoryService<BranchDto>
    {
        public Task<IEnumerable<BranchDto>> GetBySeller(string City);
    }
}
