namespace VintageTimePieceRepository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        public T Add(T entity);
        public List<T> AddRange(List<T> entity);
        public T Update(T entity);
        public IQueryable<T> FindAll();
    }
}
