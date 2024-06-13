using System.Data.Entity;
using System.Linq;
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

        public async Task<List<Timepiece>> GetAllTimepiece()
        {
            return await Task.FromResult(FindAll().Where(t => t.IsDel == false).ToList());
        }

        public async Task<List<Timepiece>> GetAllTimepieceExceptUser(User user)
        {
            return await Task.FromResult(FindAll().Where(t => t.UserId != user.UserId).ToList());
        }

        public async Task<List<TimepieceImageModel>> GetAllTimePieceWithImage()
        {
            var listImage = _context.Timepieces
                .Join(_context.TimepieceImages,
                tp => tp.TimepieceId,
                ti => ti.TimpieceId,
                (tp, ti) => new TimepieceImageModel { timepiece = tp, listImage = _context.TimepieceImages.Where(ti => ti.TimpieceId == tp.TimepieceId).ToList() })
                .ToList().Distinct().ToList();

            return await Task.FromResult(listImage);
        }

        public Task<List<TimepieceImageModel>> GetAllTimePieceWithImageExeptUser()
        {
            throw new NotImplementedException();
        }

        public async Task<PageList<Timepiece>> GetAllTimepieceWithPaging(PagingModel pagingModel)
        {
            return await Task.FromResult(PageList<Timepiece>.GetPagedList(FindAll().Where(t => t.IsDel == false).OrderBy(s => s.TimepieceId), pagingModel.PageNumber, pagingModel.PageSize));
        }

        public async Task<PageList<Timepiece>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel)
        {

            return await Task.FromResult(PageList<Timepiece>.GetPagedList(FindAll().Where(t => t.IsDel == false && t.UserId != user.UserId).OrderBy(s => s.TimepieceId), pagingModel.PageNumber, pagingModel.PageSize));
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
