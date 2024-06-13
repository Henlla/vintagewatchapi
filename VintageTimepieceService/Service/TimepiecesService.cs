using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class TimepiecesService : ITimepiecesService
    {
        private readonly ITimepieceRepository _timepieceRepository;
        private readonly IImageRepository _imageRepository;
        public TimepiecesService(ITimepieceRepository timepieceRepository,
            IImageRepository imageRepository)
        {
            _timepieceRepository = timepieceRepository;
            _imageRepository = imageRepository;
        }
        // R
        public async Task<APIResponse<List<TimepieceModel>>> GetAllTimepiece()
        {
            var result = await _timepieceRepository.GetAllTimepiece();
            return new APIResponse<List<TimepieceModel>>
            {
                Message = "Get all product success",
                isSuccess = true,
                Data = result
            };
        }
        public async Task<APIResponse<List<TimepieceModel>>> GetAllTimepieceExceptUser(User user)
        {
            var result = await _timepieceRepository.GetAllTimepieceExceptUser(user);
            return new APIResponse<List<TimepieceModel>>
            {
                Message = "Get all result success",
                isSuccess = true,
                Data = result
            };
        }
        public async Task<APIResponse<PageList<TimepieceModel>>> GetAllTimepieceWithPaging(PagingModel pageModel)
        {
            var result = await _timepieceRepository.GetAllTimepieceWithPaging(pageModel);
            if (result != null)
                return new APIResponse<PageList<TimepieceModel>>
                {
                    Message = "Get all timepiece success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<PageList<TimepieceModel>>
            {
                Message = "Don't have any timepiece",
                isSuccess = false,
                Data = result
            };
        }
        public async Task<APIResponse<PageList<TimepieceModel>>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel)
        {
            var result = await _timepieceRepository.GetAllTimepieceWithPagingExceptUser(user, pagingModel);
            if (result == null)
                return new APIResponse<PageList<TimepieceModel>>
                {
                    Message = "Don't have any time piece",
                    isSuccess = false,
                };
            return new APIResponse<PageList<TimepieceModel>>
            {
                Message = "Get all time pice with paging success",
                isSuccess = true,
                Data = result
            };
        }
        public async Task<APIResponse<TimepieceModel>> GetOneTimepiece(int id)
        {
            var result = await _timepieceRepository.GetTimepieceById(id);
            if (result == null)
            {
                return new APIResponse<TimepieceModel>
                {
                    Message = "Not found",
                    isSuccess = false,
                };
            }

            return new APIResponse<TimepieceModel>
            {
                Message = "Get timepiece success",
                isSuccess = true,
                Data = result
            };
        }
        public async Task<APIResponse<List<TimepieceModel>>> GetTimepieceByName(string name)
        {
            var result = await _timepieceRepository.GetTimepieceByName(name);
            if (result.Count > 0)
                return new APIResponse<List<TimepieceModel>>
                {
                    Message = "Get product success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<TimepieceModel>>
            {
                Message = "Don't have any product",
                isSuccess = false,
                Data = result
            };
        }

        // CUD
        public async Task<APIResponse<TimepieceModel>> DeleteTimepiece(int id)
        {
            var result = await _timepieceRepository.GetTimepieceById(id);
            if (result != null)
                return new APIResponse<TimepieceModel>
                {
                    Message = "Timepiece not exists",
                    isSuccess = false,
                };
            result.timepiece.IsDel = true;
            await _timepieceRepository.Update(result.timepiece);
            return new APIResponse<TimepieceModel>
            {
                Message = "Delete timepiece success",
                isSuccess = true,
                Data = result
            };
        }
        public async Task<APIResponse<TimepieceModel>> UpdateTimepiece(int id, TimepieceModel timePiece)
        {
            var result = await _timepieceRepository.GetTimepieceById(id);
            if (result == null)
            {
                return new APIResponse<TimepieceModel>
                {
                    Message = "Timepiece not exists",
                    isSuccess = false,
                    Data = result
                };
            }
            result.timepiece = timePiece.timepiece;
            await _timepieceRepository.Update(result.timepiece);
            return new APIResponse<TimepieceModel>
            {
                Message = "Update timepiece success",
                isSuccess = true,
                Data = result
            };
        }
    }
}
