using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly VintagedbContext _context;
        public BaseRepository(VintagedbContext context)
        {
            _context = context;
        }
        public async Task Add(T t)
        {
            await _context.Set<T>().AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T t)
        {
            _context.Set<T>().Remove(t);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Get()
        {
            return _context.Set<T>().ToList();
        }

        public async Task Update(T t)
        {
            _context.Set<T>().Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
