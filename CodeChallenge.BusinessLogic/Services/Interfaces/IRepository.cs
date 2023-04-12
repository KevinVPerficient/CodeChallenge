namespace CodeChallenge.Data.Services.Interfaces
{
    public interface IRepository <T>
    {
        public IEnumerable<T> GetAll();
        public T GetById(string Id);
        public Task<T> Create(T obj);
        public Task Update(T obj);
        public Task Delete(T obj);
    }
}
