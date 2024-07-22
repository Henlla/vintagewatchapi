using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VintageTimePieceRepository.Util;

namespace VintageTimePieceRepository.Repository
{
    public class TimepieceRepository : BaseRepository<Timepiece>, ITimepieceRepository
    {
        private readonly IConfiguration _configuration;
        private IHelper _helper { get; }
        public TimepieceRepository(VintagedbContext context,
            IConfiguration configuration,
            IHelper helper) : base(context)
        {
            _configuration = configuration;
            _helper = helper;
        }
        // R
        public async Task<List<TimepieceViewModel>> GetAllTimepiece()
        {
            var listProduct = await (from tp in _context.Timepieces
                                     join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     where tp.IsDel == false
                                     && tp.Price != null
                                     && tp.IsBuy == false
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId && tc.IsDel == false).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            return listProduct;
        }
        public async Task<List<TimepieceViewModel>> GetAllTimepieceNotEvaluate()
        {
            var listProduct = await (from tp in _context.Timepieces
                                     join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId into tpEva
                                     from eva in tpEva.DefaultIfEmpty()
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     where tp.IsDel == false
                                     && tp.TimepieceId != eva.TimepieceId
                                     && tp.Price == null
                                     && tp.IsBuy == false
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId && tc.IsDel == false).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            return listProduct;
        }
        public async Task<List<TimepieceViewModel>> GetAllTimepieceHasEvaluate(User user)
        {
            var listProduct = await (from tp in _context.Timepieces
                                     join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId into tpEva
                                     from eva in tpEva.DefaultIfEmpty()
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     where tp.IsDel == false
                                     && (tp.TimepieceId == eva.TimepieceId || tp.TimepieceId != eva.TimepieceId)
                                     && (tp.Price == null || tp.Price != null)
                                     && tp.UserId == user.UserId
                                     && tp.IsBuy == false
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         evaluation = eva.Evaluation,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            return listProduct;
        }
        public async Task<List<TimepieceViewModel>> GetAllTimepieceExceptUser(User user)
        {
            var listProduct = await (from tp in _context.Timepieces
                                     join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     where tp.IsDel == false
                                     && tp.Price != null
                                     && tp.IsBuy == false
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            return listProduct;
        }
        public async Task<TimepieceViewModel?> GetTimepieceById(int id)
        {
            var timePiece = await (from tp in _context.Timepieces
                                   join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId
                                   join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                   where tp.TimepieceId == id && tp.IsDel == false
                                   && tp.IsBuy == false
                                   select new TimepieceViewModel
                                   {
                                       timepiece = tp,
                                       mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                       images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList(),
                                       evaluation = eva.Evaluation,
                                   }).SingleOrDefaultAsync();
            return timePiece;
        }
        public async Task<List<TimepieceViewModel>> GetTimepieceByName(string name)
        {
            var listProduct = await (from tp in _context.Timepieces
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     where tp.IsDel == false && tp.TimepieceName.Contains(name)
                                     && tp.IsBuy == false
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            return listProduct;
        }
        public async Task<List<TimepieceViewModel>> GetTimepieceByNameExceptUser(string name, User user)
        {
            List<TimepieceViewModel> listProduct = new List<TimepieceViewModel>();
            if (user == null)
            {
                listProduct = await (from tp in _context.Timepieces
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     where tp.IsDel == false && tp.TimepieceName.Contains(name)
                                     && tp.IsBuy == false
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            }
            else
            {
                listProduct = await (from tp in _context.Timepieces
                                     where tp.IsDel == false && tp.TimepieceName.Contains(name) && tp.UserId != user.UserId
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            }

            return listProduct;
        }
        public async Task<List<TimepieceViewModel>> GetTimepieceByCategory(int categoryId)
        {
            var listTimePiece = await (from tc in _context.TimepieceCategories
                                       join tp in _context.Timepieces on tc.TimepieceId equals tp.TimepieceId
                                       join ca in _context.Categories on tc.CategoryId equals ca.CategoryId
                                       join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                       where ca.CategoryId == categoryId
                                       && tp.IsBuy == false
                                       select new TimepieceViewModel
                                       {
                                           timepiece = tp,
                                           mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).First(),
                                           images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                       }).ToListAsync();
            return listTimePiece;
        }

        // CUD
        public async Task<Timepiece> UploadNewTimepiece(Timepiece timepiece)
        {
            return await Add(timepiece);
        }
        public async Task<Timepiece> UpdateTimepiecePrice(int timepieceId, int price)
        {
            var currentTimepiece = await _context.Timepieces.Where(ti => ti.TimepieceId == timepieceId
                                                                    && ti.IsDel == false
                                                                    && ti.IsBuy == false).SingleAsync();
            currentTimepiece.Price = price;
            var result = await Update(currentTimepiece);
            return result;
        }
        public async Task UpdateTimepieceIsOrder(List<OrdersDetail> ordersDetails, bool isOrder)
        {
            foreach (var orderDetail in ordersDetails)
            {
                var timepiece = await _context.Timepieces.Where(time => time.TimepieceId == orderDetail.TimepieceId
                                                                    && time.IsDel == false).SingleAsync();
                timepiece.IsBuy = isOrder;
                await Update(timepiece);
            }
        }

        public async Task<Timepiece?> GetOneTimepiece(int id)
        {
            var result = await _context.Timepieces.Where(tim => tim.TimepieceId == id && tim.IsDel == false).SingleOrDefaultAsync();
            return result;
        }
    }
}
