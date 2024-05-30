namespace VintageTimePieceRepository
{
    public interface IGeneric<T>
    {
        public Task Add(T t);
        public Task Update(T t);
        public Task Delete(int id);
        public Task GetOne(int id);
        public Task<List<T>> Get();
    }
}
