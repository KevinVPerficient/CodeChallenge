namespace CodeChallenge.Business.Services.Interfaces
{
    public interface IRepositoryService <T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<IEnumerable<T>> GetById(string Id);
        public Task<IEnumerable<T>> GetByCity(string City);
        public Task<bool> Create(T obj);
        public Task Update(T obj, string id);
        public Task<bool> Delete(string Id);
    }
}
