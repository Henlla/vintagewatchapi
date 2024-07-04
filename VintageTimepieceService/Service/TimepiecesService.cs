using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class TimepiecesService : ITimepiecesService
    {
        private readonly ITimepieceRepository _timepieceRepository;
        public TimepiecesService(ITimepieceRepository timepieceRepository)
        {
            _timepieceRepository = timepieceRepository;
        }
        // R
        public async Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepiece()
        {
            var result = await Task.FromResult(_timepieceRepository.GetAllTimepiece());
            if (result.Count > 0)
                return new APIResponse<List<TimepieceViewModel>>
                {
                    Message = "Get all product success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = "Don't have any timepiece",
                isSuccess = false,
                Data = result
            };
        }
        public async Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceNotEvaluate()
        {
            var result = await Task.FromResult(_timepieceRepository.GetAllTimepieceNotEvaluate());
            if (result.Count > 0)
                return new APIResponse<List<TimepieceViewModel>>
                {
                    Message = "Get timepiece not evaluate success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = "Don't have timepiece not evaluate",
                isSuccess = false,
                Data = result
            };

        }
        public async Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceExceptUser(User user)
        {
            var result = await Task.FromResult(_timepieceRepository.GetAllTimepieceExceptUser(user));
            if (result.Count > 0)
                return new APIResponse<List<TimepieceViewModel>>
                {
                    Message = "Get all result success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = "Don't have any timepiece",
                isSuccess = false,
                Data = result
            };

        }
        //public async Task<APIResponse<PageList<TimepieceViewModel>>> GetAllTimepieceWithPaging(PagingModel pageModel)
        //{
        //    var result = await Task.FromResult(_timepieceRepository.GetAllTimepieceWithPaging(pageModel));
        //    if (result != null)
        //        return new APIResponse<PageList<TimepieceViewModel>>
        //        {
        //            Message = "Get all timepiece success",
        //            isSuccess = true,
        //            Data = result
        //        };
        //    return new APIResponse<PageList<TimepieceViewModel>>
        //    {
        //        Message = "Don't have any timepiece",
        //        isSuccess = false,
        //        Data = result
        //    };
        //}
        //public async Task<APIResponse<PageList<TimepieceViewModel>>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel)
        //{
        //    var result = await Task.FromResult(_timepieceRepository.GetAllTimepieceWithPagingExceptUser(user, pagingModel));
        //    if (result.Count == 0)
        //        return new APIResponse<PageList<TimepieceViewModel>>
        //        {
        //            Message = "Don't have any time piece",
        //            isSuccess = false,
        //            Data= result
        //        };
        //    return new APIResponse<PageList<TimepieceViewModel>>
        //    {
        //        Message = "Get all time pice with paging success",
        //        isSuccess = true,
        //        Data = result
        //    };
        //}
        public async Task<APIResponse<TimepieceViewModel>> GetOneTimepiece(int id)
        {
            var result = await Task.FromResult(_timepieceRepository.GetTimepieceById(id));
            if (result == null)
            {
                return new APIResponse<TimepieceViewModel>
                {
                    Message = "Not found",
                    isSuccess = false,
                    Data = result
                };
            }

            return new APIResponse<TimepieceViewModel>
            {
                Message = "Get timepiece success",
                isSuccess = true,
                Data = result
            };
        }
        public async Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByName(string name)
        {
            var result = await Task.FromResult(_timepieceRepository.GetTimepieceByName(name));
            if (result.Count > 0)
                return new APIResponse<List<TimepieceViewModel>>
                {
                    Message = "Get product success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = "Don't have any product",
                isSuccess = false,
                Data = result
            };
        }
        public async Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByNameExceptUser(string name, User user)
        {
            var result = await Task.FromResult(_timepieceRepository.GetTimepieceByNameExceptUser(name, user));
            if (result.Count > 0)
                return new APIResponse<List<TimepieceViewModel>>
                {
                    Message = "Get timepiece success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = "Don't find the timepiece",
                isSuccess = false,
                Data = result
            };
        }

        // CUD
        public async Task<APIResponse<TimepieceViewModel>> DeleteTimepiece(int id)
        {
            var timepiece = await Task.FromResult(_timepieceRepository.GetTimepieceById(id));
            if (timepiece != null)
                return new APIResponse<TimepieceViewModel>
                {
                    Message = "Timepiece not exists",
                    isSuccess = false,
                    Data = timepiece
                };
            timepiece.timepiece.IsDel = true;
            var result = await Task.FromResult(_timepieceRepository.Update(timepiece.timepiece));
            if (result == null)
                return new APIResponse<TimepieceViewModel>
                {
                    Message = "Delete timepiece fail",
                    isSuccess = false,
                };
            return new APIResponse<TimepieceViewModel>
            {
                Message = "Delete timepiece success",
                isSuccess = true
            };
        }
        public async Task<APIResponse<TimepieceViewModel>> UpdateTimepiece(int id, TimepieceViewModel timePiece)
        {
            var timepiece = await Task.FromResult(_timepieceRepository.GetTimepieceById(id));
            if (timepiece == null)
            {
                return new APIResponse<TimepieceViewModel>
                {
                    Message = "Timepiece not exists",
                    isSuccess = false,
                    Data = timepiece
                };
            }
            timepiece.timepiece = timePiece.timepiece;
            var result = await Task.FromResult(_timepieceRepository.Update(timepiece.timepiece));
            if (result == null)
                return new APIResponse<TimepieceViewModel>
                {
                    Message = "Update timepiece fail",
                    isSuccess = true,
                    Data = timepiece
                };
            return new APIResponse<TimepieceViewModel>
            {
                Message = "Update timepiece success",
                isSuccess = true,
                Data = timepiece
            };
        }
        public async Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByCategory(int categoryId)
        {
            var result = await Task.FromResult(_timepieceRepository.GetTimepieceByCategory(categoryId));
            if (result.Count > 0)
                return new APIResponse<List<TimepieceViewModel>>
                {
                    Message = "Get timepiece success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = "Don't have timepiece",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<Timepiece>> UploadNewTimepiece(Timepiece timepiece)
        {
            var result = await Task.FromResult(_timepieceRepository.UploadNewTimepiece(timepiece));
            if (result != null)
                return new APIResponse<Timepiece>
                {
                    Message = "Upload timepiece success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<Timepiece>
            {
                Message = "Upload timepiece fail",
                isSuccess = false,
                Data = result
            };
        }
    }
}
