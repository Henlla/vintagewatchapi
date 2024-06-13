using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Firebase.Storage;
using Microsoft.Extensions.Configuration;
using Google.Apis.Auth.OAuth2;

namespace VintageTimePieceRepository.Repository
{
    public class TimepieceRepository : BaseRepository<Timepiece>, ITimepieceRepository
    {
        private readonly IConfiguration _configuration;
        public TimepieceRepository(VintagedbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }
        // R
        public async Task<List<TimepieceModel>> GetAllTimepiece()
        {
            var listProduct = await (from tp in _context.Timepieces
                                     where tp.IsDel == false
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     select new TimepieceModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).Skip(1).ToList()
                                     }).ToListAsync();
            return await Task.FromResult(listProduct);
        }

        public async Task<List<TimepieceModel>> GetAllTimepieceExceptUser(User user)
        {
            var listProduct = await (from tp in _context.Timepieces
                                     where tp.IsDel == false && tp.UserId != user.UserId
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     select new TimepieceModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).Skip(1).ToList()
                                     }).ToListAsync();
            return await Task.FromResult(listProduct);
        }

        public async Task<PageList<TimepieceModel>> GetAllTimepieceWithPaging(PagingModel pagingModel)
        {
            var listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false
                               orderby tp.TimepieceId
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).Skip(1).ToList()
                               });
            return await Task.FromResult(PageList<TimepieceModel>.GetPagedList(listProduct, pagingModel.PageNumber, pagingModel.PageSize));
        }

        public async Task<PageList<TimepieceModel>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel)
        {
            var listProduct = (from tp in _context.Timepieces
                               where tp.IsDel == false && tp.UserId != user.UserId
                               orderby tp.TimepieceId
                               join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                               select new TimepieceModel
                               {
                                   timepiece = tp,
                                   mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                   images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).Skip(1).ToList()
                               });
            return await Task.FromResult(PageList<TimepieceModel>.GetPagedList(listProduct, pagingModel.PageNumber, pagingModel.PageSize));
        }

        public async Task<TimepieceModel?> GetTimepieceById(int id)
        {
            var timePiece = await (from tp in _context.Timepieces
                                   where tp.TimepieceId == id && tp.IsDel == false
                                   join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                   select new TimepieceModel
                                   {
                                       timepiece = tp,
                                       mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                       images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).Skip(1).ToList()
                                   }).SingleOrDefaultAsync();
            return await Task.FromResult(timePiece);
        }

        public async Task<List<TimepieceModel>> GetTimepieceByName(string name)
        {
            var listProduct = await (from tp in _context.Timepieces
                                     where tp.IsDel == false && tp.TimepieceName.Contains(name)
                                     join ti in _context.TimepieceImages on tp.TimepieceId equals ti.TimepieceId into images
                                     select new TimepieceModel
                                     {
                                         timepiece = tp,
                                         mainImage = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).FirstOrDefault(),
                                         images = images.Where(img => img.IsDel == false).OrderBy(img => img.TimepieceImageId).Skip(1).ToList()
                                     }).ToListAsync();
            return await Task.FromResult(listProduct);
        }

        public async Task<string>? UploadImage(IFormFile files)
        {
            var bucket = _configuration["Firebase:bucket"];
            if (files == null || files.Length == 0)
            {
                return null;
            }
            var stream = files.OpenReadStream();
            var storage = new FirebaseStorage(bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = async () =>
                {
                    var googleCredential = GoogleCredential.FromFile(_configuration["Firebase:jsonPath"]);
                    var accessToken = await googleCredential.UnderlyingCredential.GetAccessTokenForRequestAsync();
                    return accessToken;
                }
            });
            var task = storage.Child("images").PutAsync(stream);
            var url = await task;
            return url;
        }
    }
}
