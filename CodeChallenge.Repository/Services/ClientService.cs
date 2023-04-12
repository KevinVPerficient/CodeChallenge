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
        public async Task Create(ClientDto obj)
        {
            validator.ValidateAndThrow(obj);
            if (obj.Branches == null) throw new Exception("Please add at least one branch");

            var client = _mapper.Map<Client>(obj);
            await _clientRepository.Create(client);
        }

        public bool Delete(string Id)
        {
            var clientToDelete = _clientRepository.GetById(Id);      
            if (clientToDelete == null) return false;
            _clientRepository.Delete(clientToDelete);
            return true;
        }

        public IEnumerable<ClientDto> GetAll()
        {
            var clients = _mapper.Map<List<ClientDto>>(_clientRepository.GetAll());
            return clients;
        }

        public ClientDto GetById(string Id)
        {
            var client = _mapper.Map<ClientDto>(_clientRepository.GetById(Id));
            return client;
        }

        public async Task Update(ClientDto obj, string id)
        {
            validator.ValidateAndThrow(obj);

            var client = _clientRepository.GetById(id);

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
