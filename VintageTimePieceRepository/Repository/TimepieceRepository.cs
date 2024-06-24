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
        public List<TimepieceViewModel> GetAllTimepiece()
        {
            var listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceViewModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   category = _context.TimepieceCategories.Where(tc => tc.TimepieceId == tp.TimepieceId).OrderBy(tc => tc.TimepieceCategoryId).ToList(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                               }).ToList();
            return listProduct;
        }

        public List<TimepieceViewModel> GetAllTimepieceExceptUser(User user)
        {
            var listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false && tp.UserId != user.UserId
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceViewModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                               }).ToList();
            return listProduct;
        }

        public PageList<TimepieceViewModel> GetAllTimepieceWithPaging(PagingModel pagingModel)
        {
            var listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false
                               orderby tp.TimepieceId
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceViewModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                               });
            return PageList<TimepieceViewModel>.GetPagedList(listProduct, pagingModel.PageNumber, pagingModel.PageSize);
        }

        public PageList<TimepieceViewModel> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel)
        {
            var listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false && tp.UserId != user.UserId
                               orderby tp.TimepieceId
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceViewModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                               });
            return PageList<TimepieceViewModel>.GetPagedList(listProduct, pagingModel.PageNumber, pagingModel.PageSize);
        }
        public TimepieceViewModel? GetTimepieceById(int id)
        {
            var timePiece = (from tp in _context.Timepieces
                             where tp.TimepieceId == id && tp.IsDel == false
                             join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                             select new TimepieceViewModel
                             {
                                 timepiece = tp,
                                 mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                 images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                             }).SingleOrDefault();
            return timePiece;
        }
        public List<TimepieceViewModel> GetTimepieceByName(string name)
        {
            var listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false && tp.TimepieceName.Contains(name)
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceViewModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                               }).ToList();
            return listProduct;
        }
        public List<TimepieceViewModel> GetTimepieceByNameExceptUser(string name, User user)
        {
            List<TimepieceViewModel> listProduct = new List<TimepieceViewModel>();
            if (user == null)
            {
                listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false && tp.TimepieceName.Contains(name)
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceViewModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                               }).ToList();
            }
            else
            {
                listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false && tp.TimepieceName.Contains(name) && tp.UserId != user.UserId
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceViewModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                               }).ToList();
            }

            return listProduct;
        }
        public List<TimepieceViewModel> GetTimepieceByCategory(int categoryId)
        {
            var listTimePiece = (from tc in _context.TimepieceCategories
                                 join tp in _context.Timepieces on tc.TimepieceId equals tp.TimepieceId
                                 join ca in _context.Categories on tc.CategoryId equals ca.CategoryId
                                 join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                 where ca.CategoryId == categoryId
                                 select new TimepieceViewModel
                                 {
                                     timepiece = tp,
                                     mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).First(),
                                     images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).ToList()
                                 }).ToList();
            return listTimePiece;
        }


        // CUD
        public Timepiece UploadNewTimepiece(Timepiece timepiece)
        {

            throw new NotImplementedException();
        }
    }
}
