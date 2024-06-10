namespace VintageTimePieceRepository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public IQueryable<T> FindAll();
    }
}
