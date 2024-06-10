﻿using VintageTimepieceModel.Models;
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

        public async Task<APIResponse<Timepiece>> CreateNewTimepiece(Timepiece timepiece)
        {
            var result = await _timepieceRepository.Add(timepiece);
            return new APIResponse<Timepiece>
            {
                Message = "Create Timepiece success",
                isSuccess = true,
                Data = result
            };
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

        public async Task<APIResponse<PageList<Timepiece>>> GetAllTimepiece(PagingModel pageModel)
        {
            var result = await _timepieceRepository.GetAllTimepiece(pageModel);
            if (result != null)
                return new APIResponse<PageList<Timepiece>>
                {
                    Message = "Get all timepiece success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<PageList<Timepiece>>
            {
                Message = "Get all timepiece success",
                isSuccess = false,
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
    }
}
