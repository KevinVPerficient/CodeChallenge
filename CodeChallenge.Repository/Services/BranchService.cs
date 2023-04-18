using AutoMapper;
using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.DTOs;
using CodeChallenge.Data.DTOs.Validations;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services.Interfaces;
using FluentValidation;

namespace CodeChallenge.Business.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private BranchValdations validator = new BranchValdations();

        public BranchService(IBranchRepository branchRepository, IMapper mapper, IClientRepository clientRepository)
        {
            _branchRepository = branchRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public Task<bool> Create(BranchDto obj)
        {
            throw new Exception("Insert client's document number");
        }

        public async Task<bool> Create(BranchDto obj, string doc)
        {
            validator.ValidateAndThrow(obj);
            var branch = _mapper.Map<Branch>(obj);
            var client = (await _clientRepository.GetById(doc)).FirstOrDefault() ?? throw new Exception("Client doesn't exist");
            branch.Client = client;
            await _branchRepository.Create(branch);
            return true;
        }

        public async Task<bool> Delete(string Id)
        {
            throw new Exception("Insert client's document number");
        }

        public async Task<bool> Delete(string Id, string ClientDoc)
        {
            var branchToDelete = (await(_branchRepository.GetById(Id))).Where(x => x.Client.DocNumber == ClientDoc).FirstOrDefault();
            if (branchToDelete == null) return false;
            var CLientsBranch = _branchRepository.GetAll().Where(x => x.ClientGuid == branchToDelete.ClientGuid);
            if (CLientsBranch.Count() == 1) throw new Exception("The client cannot be left without a branch");
            await _branchRepository.Delete(branchToDelete);
            return true;
        }

        public IEnumerable<BranchDto> GetAll()
        {
            var branches = _mapper.Map<List<BranchDto>>(_branchRepository.GetAll());
            return branches;
        }

        public async Task<IEnumerable<BranchDto>> GetByCity(string City)
        {
            var branch = _mapper.Map<IEnumerable<BranchDto>>(await _branchRepository.GetByCity(City));
            return branch;
        }

        public async Task<IEnumerable<BranchDto>> GetByClientDoc(string doc)
        {
            var branches = _mapper.Map<IEnumerable<BranchDto>>(await _branchRepository.GetByClientDocument(doc));
            return branches;
        }

        public async Task<IEnumerable<BranchDto>> GetById(string Id)
        {
            var branch = _mapper.Map<IEnumerable<BranchDto>>(await _branchRepository.GetById(Id));
            return branch;
        }

        public Task Update(BranchDto obj, string id)
        {
            throw new Exception("Insert client's document number");
        }

        public async Task Update(BranchDto obj, string id, string doc)
        {
            validator.ValidateAndThrow(obj);
            var branch = (await _branchRepository.GetById(id))
                .Where(x => x.Client.DocNumber == doc)
                .FirstOrDefault() ?? throw new Exception("Branch doesn't exist");

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
