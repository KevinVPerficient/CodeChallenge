using AutoMapper;
using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.DTOs;
using CodeChallenge.Data.DTOs.Validations;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services.Interfaces;
using FluentValidation;

namespace CodeChallenge.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ClientValidations validator = new ClientValidations();
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<bool> Create(ClientDto obj)
        {
            validator.ValidateAndThrow(obj);
            if (obj.Branches == null) throw new Exception("Please add at least one branch");

            var client = _mapper.Map<Client>(obj);
            await _clientRepository.Create(client);
            return true;
        }

        public async Task<bool> Delete(string Id)
        {
            var clientToDelete = (await (_clientRepository.GetById(Id)));      
            if (clientToDelete.Count() == 0) return false;
            await _clientRepository.Delete(clientToDelete.FirstOrDefault());
            return true;
        }

        public Task<IEnumerable<ClientDto>> GetAll()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<ClientDto>>(_clientRepository.GetAll()));
        }

        public async Task<IEnumerable<ClientDto>> GetByCity(string City)
        {
            
            return _mapper.Map<IEnumerable<ClientDto>>(await _clientRepository.GetByCity(City));
        }

        public async Task<IEnumerable<ClientDto>> GetById(string Id)
        {
            return _mapper.Map<IEnumerable<ClientDto>>(await _clientRepository.GetById(Id));
        }

        public async Task<IEnumerable<ClientDto>> GetBySeller(string code)
        {
            var clients = _mapper.Map<List<ClientDto>>(await _clientRepository.GetBySeller(code));
            return clients;
        }

        public async Task Update(ClientDto obj, string id)
        {
            validator.ValidateAndThrow(obj);

            var client = (await _clientRepository.GetById(id)).FirstOrDefault() ?? throw new Exception("Client doesn't exist");
            client.ClientType = obj.ClientType;
            client.FullName = obj.FullName;
            client.CompanyName = obj.CompanyName;
            client.DocType = obj.DocType;
            client.DocNumber = obj.DocNumber;
            client.Address = obj.Address;
            client.City = obj.City;
            client.Country = obj.Country;
            client.PhoneNumber = obj.PhoneNumber;
            client.CellPhoneNumber = obj.CellPhoneNumber;
            client.Email = obj.Email;

            await _clientRepository.Update(client);
        }
    }
}
