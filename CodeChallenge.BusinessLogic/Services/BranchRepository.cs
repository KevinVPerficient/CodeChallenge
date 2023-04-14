using CodeChallenge.Data.Data;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data.Services
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ProjectDbContext _context;
        public BranchRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<Branch> Create(Branch obj)
        {
            _context.Branches.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(Branch obj)
        {
            _context.Branches.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Branch> GetAll()
        {
            return _context.Branches.ToList();
        }

        public async Task<IEnumerable<Branch>> GetByCity(string City)
        {
            return await _context.Branches.Where(x => x.City == City).ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetById(string Id)
        {
            return await _context.Branches.Where(x => x.Code == Id).ToListAsync();
        }

        public async Task Update(Branch obj)
        {
            //_context.Branches.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
