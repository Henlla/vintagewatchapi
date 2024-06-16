using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel.Models;
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
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public List<T> AddRange(List<T> entity)
        {
            _context.Set<T>().AddRange(entity);
            _context.SaveChanges();
            return entity;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

    }
}
