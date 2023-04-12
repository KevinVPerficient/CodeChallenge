using AutoMapper;
using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.DTOs;
using CodeChallenge.Data.DTOs.Validations;
using CodeChallenge.Data.Services;
using CodeChallenge.Data.Services.Interfaces;
using FluentValidation;

namespace CodeChallenge.Business.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public BranchService(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }
        public Task Create(BranchDto obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string Id)
        {
            var branchToDelete = _branchRepository.GetById(Id);
            if (branchToDelete == null) return false;
            _branchRepository.Delete(branchToDelete);
            return true;
        }

        public IEnumerable<BranchDto> GetAll()
        {
            var branches = _mapper.Map<List<BranchDto>>(_branchRepository.GetAll());
            return branches;
        }

        public BranchDto GetById(string Id)
        {
            var branch = _mapper.Map<BranchDto>(_branchRepository.GetById(Id));
            return branch;
        }

        public async Task Update(BranchDto obj, string id)
        {
            var validator = new BranchValdations();
            validator.ValidateAndThrow(obj);
            var branch = _branchRepository.GetById(id);
            
            branch.Name = obj.Name;
            branch.SellerCode = obj.SellerCode;
            branch.Credit = obj.Credit;
            branch.Address = obj.Address;
            branch.City = obj.City;
            branch.Country = obj.Country;
            branch.PhoneNumber = obj.PhoneNumber;
            branch.CellPhoneNumber = obj.CellPhoneNumber;
            branch.Email = obj.Email;

            await _branchRepository.Update(branch);
        }
    }
}
