using AutoMapper;
using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.DTOs;
using CodeChallenge.Data.DTOs.Validations;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services;
using CodeChallenge.Data.Services.Interfaces;
using FluentValidation;

namespace CodeChallenge.Business.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private BranchValdations validator = new BranchValdations();

        public BranchService(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }
        public async Task<bool> Create(BranchDto obj)
        {
            validator.ValidateAndThrow(obj);

            var branch = _mapper.Map<Branch>(obj);
            await _branchRepository.Create(branch);
            return true;
        }

        public async Task<bool> Delete(string Id)
        {
            var branchToDelete = (await(_branchRepository.GetById(Id))).FirstOrDefault();
            if (branchToDelete == null) return false;
            var CLientsBranch = _branchRepository.GetAll().Where(x => x.ClientGuid ==  branchToDelete.ClientGuid);
            if (CLientsBranch.Count() == 1) throw new Exception("The client cannot be left without a branch");  
            await _branchRepository.Delete(branchToDelete);
            return true;
        }

        public IEnumerable<BranchDto> GetAll()
        {
            var branches = _mapper.Map<List<BranchDto>>(_branchRepository.GetAll());
            return branches;
        }

        public IEnumerable<BranchDto> GetByCity(string City)
        {
            var branch = _mapper.Map<IEnumerable<BranchDto>>(_branchRepository.GetByCity(City).Result);
            return branch;
        }

        public IEnumerable<BranchDto> GetById(string Id)
        {
            var branch = _mapper.Map<IEnumerable<BranchDto>>(_branchRepository.GetById(Id).Result);
            return branch;
        }

        public async Task Update(BranchDto obj, string id)
        {
            validator.ValidateAndThrow(obj);
            var branch = (await _branchRepository.GetById(id)).FirstOrDefault() ?? throw new Exception("Branch doesn't exist");

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
