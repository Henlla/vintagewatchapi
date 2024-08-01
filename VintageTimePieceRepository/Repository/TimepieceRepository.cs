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
                                     where tp.IsDel == false && tp.IsBuy == false
                                     && tp.Price != null
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId && tc.IsDel == false).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            return listProduct;
        }
        public Task<PageList<TimepieceViewModel>> GetAllProductWithPaginate(PagingModel pagingModel, User user)
        {
            var listProduct = (from tp in _context.Timepieces
                               join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               where tp.IsDel == false && tp.IsBuy == false
                               && tp.Price != null
                               && tp.User != user
                               select new TimepieceViewModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId && tc.IsDel == false).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                               });
            return Task.FromResult(PageList<TimepieceViewModel>.GetPagedList(listProduct, pagingModel.PageNumber, pagingModel.PageSize));
        }
        public Task<PageList<TimepieceViewModel>> GetAllTimepieceByCategoryNameWithPaging(string categoryName, User user, PagingModel pagingModel)
        {
            var result = (from tp in _context.Timepieces
                          join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId
                          join catTi in _context.TimepieceCategories on tp.TimepieceId equals catTi.TimepieceId
                          join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                          where tp.IsDel == false && tp.IsBuy == false
                          && tp.Price != null
                          && tp.User != user
                          && catTi.Category.CategoryName == categoryName
                          select new TimepieceViewModel
                          {
                              timepiece = tp,
                              mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                              category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId && tc.IsDel == false).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                              images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                          });
            return Task.FromResult(PageList<TimepieceViewModel>.GetPagedList(result, pagingModel.PageNumber, pagingModel.PageSize));
        }
        public Task<List<TimepieceViewModel>> GetAllTimepieceByName(string name, User user)
        {
            var result = (from tp in _context.Timepieces
                          join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId
                          join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                          where tp.IsDel == false && tp.IsBuy == false
                          && tp.Price != null
                          && tp.User != user
                          && tp.TimepieceName.Contains(name)
                          select new TimepieceViewModel
                          {
                              timepiece = tp,
                              mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                              category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId && tc.IsDel == false).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                              images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                          }).ToListAsync();
            return result;
        }
        public async Task<List<TimepieceViewModel>> GetAllTimepieceNotEvaluate(string keyword)
        {
            var listProduct = await (from tp in _context.Timepieces
                                     join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId into tpEva
                                     from eva in tpEva.DefaultIfEmpty()
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     where tp.IsDel == false
                                     && (tp.TimepieceName.Contains(keyword)
                                     || tp.User.FirstName.Contains(keyword)
                                     || tp.User.LastName.Contains(keyword)
                                     || tp.Brand.BrandName.Contains(keyword)
                                     || keyword == null)
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
                                     && tp.User == user
                                     //&& tp.IsBuy == false
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
        public async Task<Timepiece?> GetOneTimepiece(int id)
        {
            var result = await _context.Timepieces.Where(tim => tim.TimepieceId == id && tim.IsDel == false).SingleOrDefaultAsync();
            return result;
        }
        public async Task<List<TimepieceViewModel>> GetProductByNameAndCategory(string name, int categoryId, User user)
        {
            var listProduct = await (from tp in _context.Timepieces
                                     join eva in _context.TimepieceEvaluations on tp.TimepieceId equals eva.TimepieceId
                                     join cateTp in _context.TimepieceCategories on tp.TimepieceId equals cateTp.TimepieceId
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     where tp.IsDel == false
                                     && tp.Price != null
                                     && tp.IsBuy == false
                                     && tp.User != user
                                     && (cateTp.Category.CategoryId == categoryId || categoryId == 0)
                                     && (tp.TimepieceName.Contains(name) || name == null)
                                     select new TimepieceViewModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId && tc.IsDel == false).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                     }).ToListAsync();
            return listProduct;
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
        public async Task<Timepiece> UpdateTimepieceBuy(int timepieceId, bool status)
        {
            var product = _context.Timepieces.Where(t => t.TimepieceId == timepieceId && t.IsDel == false).SingleOrDefault();
            product.IsBuy = status;
            return await Update(product);
        }

        public async Task<Timepiece> UpdateTimepieceIsReceive(int timepieceId, bool status)
        {
            var timpiece = _context.Timepieces
                .Where(t => t.TimepieceId == timepieceId
                && t.IsDel == false
                && t.IsBuy == false
                && t.isReceive == false
                )
                .SingleOrDefault();
            timpiece.isReceive = true;
            return await Update(timpiece);
        }
    }
}
