using CodeChallenge.Data.Models;

namespace CodeChallenge.Data.Services.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        public Task<List<Client>> GetBySeller(string code);
    }
}
