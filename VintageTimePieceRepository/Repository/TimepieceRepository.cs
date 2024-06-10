using System.Data.Entity;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class TimepieceRepository : BaseRepository<Timepiece>, ITimepieceRepository
    {
        public TimepieceRepository(VintagedbContext context) : base(context)
        {
        }

        public async Task<PageList<Timepiece>> GetAllTimepiece(PagingModel pagingModel)
        {
            return await Task.FromResult(PageList<Timepiece>.GetPagedList(FindAll().Where(t => t.IsDel == false).OrderBy(s => s.TimepieceId), pagingModel.PageNumber, pagingModel.PageSize));
        }

        public async Task<Timepiece> GetTimepieceById(int id)
        {
            return await Task.FromResult(await _context.Timepieces.FirstOrDefaultAsync(t => t.TimepieceId == id && t.IsDel == false));
        }

        public async Task<List<Timepiece>> GetTimepieceByName(string name)
        {
            return await Task.FromResult(await FindAll().Where(t => t.TimepieceName.Contains(name)).ToListAsync());
        }
    }
}
