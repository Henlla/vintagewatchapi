using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface ITimepiecesService
    {
        // R
        public Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepiece();
        public Task<APIResponse<PageList<TimepieceViewModel>>> GetAllTimepieceWithPageList(PagingModel pagingModel, User user);
        public Task<APIResponse<PageList<TimepieceViewModel>>> GetAllTimepieceByCategoryNameWithPaging(string categoryName, User user, PagingModel pagingModel);
        public Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceByName(string name, User user);
        public Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceNotEvaluate(string keyword);
        public Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceExceptUser(User user);
        public Task<APIResponse<TimepieceViewModel>> GetOneTimepiece(int id);
        //public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByName(string name);
        //public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByNameExceptUser(string name, User user);
        //public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByCategory(int categoryId);
        public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceHasEvaluate(User user);
        public Task<APIResponse<List<TimepieceViewModel>>> GetProductByNameAndCategory(string name, int categoryId, User user);

        // CUD
        public Task<APIResponse<Timepiece>> DeleteTimepiece(int id);
        public Task<APIResponse<Timepiece>> UploadNewTimepiece(Timepiece timepiece);
        //public Task<APIResponse<TimepieceViewModel>> UpdateTimepiece(int id, Timepiece timepiece);
        public Task<APIResponse<Timepiece>> UpdateTimepiecePrice(int timpieceId, int price);
        public Task<APIResponse<string>> UpdateTimepieceOrder(List<OrdersDetail> ordersDetails, bool isOrder);
        public Task<APIResponse<Timepiece>> UpdateTimepieceBuy(int timepieceId, bool status);

    }
}
