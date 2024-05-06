using CodeChallenge.Business.DTOs;
using CodeChallenge.Data.DTOs;

namespace CodeChallenge.Business.Services.Interfaces
{
    public interface IBranchService : IRepositoryService <BranchDto>
    {
        public Task Update(BranchDto obj, string id, string doc);
        public Task UpdateSellerCode(SellerCodeDto sellerCode, string id, string doc);
        public Task<bool> Create(BranchDto obj, string doc);
        public Task<IEnumerable<BranchDto>> GetByClientDoc(string doc);
        public Task<bool> Delete(string Id, string ClientDoc);
    }
}
