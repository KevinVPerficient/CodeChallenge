using CodeChallenge.Data.Data;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data.Services
{
    public class ClientRepository : IClientRepository
    {
        private readonly ProjectDbContext _context;
        public ClientRepository(ProjectDbContext context) 
        {
            _context = context;
        }
        public async Task<Client> Create(Client obj)
        {
            _context.Clients.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(Client obj)
        {
            _context.Clients.Remove(obj);
            await _context.SaveChangesAsync();
        }
                                                                    
        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.Include(x => x.Branches).ToList();
        }

        public async Task<IEnumerable<Client>> GetByCity(string City)
        {
            return await _context.Clients.Include(x => x.Branches).Where(x => x.City == City).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetById(string Id)
        {                                                                                       
            return await _context.Clients.Include(x => x.Branches).Where(x => x.DocNumber == Id).ToListAsync();
        }

        public async Task<List<Client>> GetBySeller(string code)
        {
            var branches = await _context.Branches.Include(x => x.Client).Where(x => x.SellerCode == code).ToListAsync();
            List<Client> clients = new List<Client>();
            if(branches == null) return clients; 
            foreach (var branch in branches)
            {
                clients.Add(branch.Client);
            }
            return clients;
        }

        public async Task Update(Client obj)
        {
            //_context.Clients.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
