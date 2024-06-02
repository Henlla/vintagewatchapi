namespace VintageTimePieceRepository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        public Task Add(T t);
        public Task Update(T t);
        public Task Delete(T t);
        public Task<IEnumerable<T>> Get();
    }
}
