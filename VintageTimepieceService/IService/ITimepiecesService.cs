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
        public Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceNotEvaluate();
        public Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceExceptUser(User user);
        public Task<APIResponse<TimepieceViewModel>> GetOneTimepiece(int id);
        public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByName(string name);
        public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByNameExceptUser(string name, User user);
        public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByCategory(int categoryId);
        public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceHasEvaluate(User user);
        // C
        public Task<APIResponse<Timepiece>> DeleteTimepiece(int id);
        public Task<APIResponse<Timepiece>> UploadNewTimepiece(Timepiece timepiece);
        public Task<APIResponse<TimepieceViewModel>> UpdateTimepiece(int id, TimepieceViewModel timepiece);
        public Task<APIResponse<Timepiece>> UpdateTimepiecePrice(int timpieceId, int price);
        public Task<APIResponse<string>> UpdateTimepieceOrder(List<OrdersDetail> ordersDetails, bool isOrder);

    }
}
