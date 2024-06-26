﻿using CodeChallenge.Data.Models;

namespace CodeChallenge.Data.Services.Interfaces
{
    public interface IBranchRepository : IRepository<Branch>
    {
        public Task<IEnumerable<Branch>> GetByClientDocument(string doc);
    }
}
