using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected VintagedbContext _context { get; }
        public BaseRepository(VintagedbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
