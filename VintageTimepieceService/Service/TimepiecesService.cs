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

        public async Task<APIResponse<Timepiece>> DeleteTimepiece(int id)
        {
            var result = await _timepieceRepository.GetTimepieceById(id);
            if (result != null)
                return new APIResponse<Timepiece>
                {
                    Message = "Timepiece not exists",
                    isSuccess = false,
                };
            result.IsDel = true;
            await _timepieceRepository.Update(result);
            return new APIResponse<Timepiece>
            {
                Message = "Delete timepiece success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<List<Timepiece>>> GetAllTimepiece()
        {
            var result = await _timepieceRepository.GetAllTimepiece();
            return new APIResponse<List<Timepiece>>
            {
                Message = "Get all product success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<List<Timepiece>>> GetAllTimepieceExceptUser(User user)
        {
            var result = await _timepieceRepository.GetAllTimepieceExceptUser(user);
            return new APIResponse<List<Timepiece>>
            {
                Message = "Get all result success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<PageList<Timepiece>>> GetAllTimepieceWithPaging(PagingModel pageModel)
        {
            var result = await _timepieceRepository.GetAllTimepieceWithPaging(pageModel);
            if (result != null)
                return new APIResponse<PageList<Timepiece>>
                {
                    Message = "Get all timepiece success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<PageList<Timepiece>>
            {
                Message = "Don't have any timepiece",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<PageList<Timepiece>>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel)
        {
            var result = await _timepieceRepository.GetAllTimepieceWithPagingExceptUser(user, pagingModel);
            if (result == null)
                return new APIResponse<PageList<Timepiece>>
                {
                    Message = "Don't have any time piece",
                    isSuccess = false,
                };
            return new APIResponse<PageList<Timepiece>>
            {
                Message = "Get all time pice with paging success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<Timepiece>> GetOneTimepiece(int id)
        {
            var result = await _timepieceRepository.GetTimepieceById(id);
            if (result == null)
            {
                return new APIResponse<Timepiece>
                {
                    Message = "Not found",
                    isSuccess = false,
                };
            }

            return new APIResponse<Timepiece>
            {
                Message = "Get timepiece success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<Timepiece>> UpdateTimepiece(int id, Timepiece timepiece)
        {
            var result = await _timepieceRepository.GetTimepieceById(id);
            if (result == null)
            {
                return new APIResponse<Timepiece>
                {
                    Message = "Timepiece not exists",
                    isSuccess = false,
                };
            }
            result = timepiece;
            await _timepieceRepository.Update(result);
            return new APIResponse<Timepiece>
            {
                Message = "Update timepiece success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<Timepiece>> UploadNewTimepiece(Timepiece timepiece)
        {
            var result = await _timepieceRepository.Add(timepiece);
            if (result != null)
            {
                return new APIResponse<Timepiece>
                {
                    Message = "Upload timepiece success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<Timepiece>
            {
                Message = "Upload timepiece fail",
                isSuccess = false,
            };

        }
    }
}
