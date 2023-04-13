namespace CodeChallenge.Business.Services.Interfaces
{
    public interface IRepositoryService <T>
    {
        public IEnumerable<T> GetAll();
        public T GetById(string Id);
        public Task<bool> Create(T obj);
        public Task Update(T obj, string id);
        public bool Delete(string Id);
    }
}
