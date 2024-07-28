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
            var result = await _timepieceRepository.GetAllTimepiece();
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }

            var message = isSuccess ? "Get all product success" : "Don't have any timepiece";
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        public async Task<APIResponse<PageList<TimepieceViewModel>>> GetAllTimepieceWithPageList(PagingModel pagingModel, User user)
        {
            var result = await _timepieceRepository.GetAllProductWithPaginate(pagingModel, user);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all product success" : "Don't have any product";
            return new APIResponse<PageList<TimepieceViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result,
                TotalCount = result?.TotalCount,
                TotalPages = result?.TotalPages,
                CurrentPage = result?.CurrentPage,
                PageSize = result?.PageSize,
            };
        }
        public async Task<APIResponse<PageList<TimepieceViewModel>>> GetAllTimepieceByCategoryNameWithPaging(string categoryName, User user, PagingModel pagingModel)
        {
            var result = await _timepieceRepository.GetAllTimepieceByCategoryNameWithPaging(categoryName, user, pagingModel);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all product success" : "Don't have any product";
            return new APIResponse<PageList<TimepieceViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result,
                TotalCount = result.Count,
                TotalPages = result.TotalPages,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize
            };
        }
        public async Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceByName(string name, User user)
        {
            var result = await _timepieceRepository.GetAllTimepieceByName(name, user);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all timepiece success" : "Don't find any timepiece";
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result,
            };
        }
        public async Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceNotEvaluate(string keyword)
        {
            var result = await _timepieceRepository.GetAllTimepieceNotEvaluate(keyword);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get timepiece not evaluate success" : "Don't have timepiece not evaluate";

            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };

        }
        public async Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceExceptUser(User user)
        {
            var result = await _timepieceRepository.GetAllTimepieceExceptUser(user);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }

            var message = isSuccess ? "Get all result success" : "Don't have any timepiece";

            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };

        }
        public async Task<APIResponse<TimepieceViewModel>> GetOneTimepiece(int id)
        {
            var result = await _timepieceRepository.GetTimepieceById(id);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }

            var message = isSuccess ? "Get timepiece success" : "Not found";

            return new APIResponse<TimepieceViewModel>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        //public async Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByName(string name)
        //{
        //    var result = await _timepieceRepository.GetTimepieceByName(name);
        //    bool isSuccess = false;
        //    if (result.Count > 0)
        //    {
        //        isSuccess = true;
        //    }
        //    else
        //    {
        //        isSuccess = false;
        //    }
        //    var message = isSuccess ? "Get product success" : "Don't have any product";

        //    return new APIResponse<List<TimepieceViewModel>>
        //    {
        //        Message = message,
        //        isSuccess = isSuccess,
        //        Data = result
        //    };
        //}
        //public async Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByNameExceptUser(string name, User user)
        //{
        //    var result = await _timepieceRepository.GetTimepieceByNameExceptUser(name, user);
        //    bool isSuccess = false;
        //    if (result.Count > 0)
        //    {
        //        isSuccess = true;
        //    }
        //    else
        //    {
        //        isSuccess = false;
        //    }
        //    var message = isSuccess ? "Get timepiece success" : "Don't find the timepiece";

        //    return new APIResponse<List<TimepieceViewModel>>
        //    {
        //        Message = message,
        //        isSuccess = isSuccess,
        //        Data = result
        //    };
        //}
        public async Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceHasEvaluate(User user)
        {
            var result = await _timepieceRepository.GetAllTimepieceHasEvaluate(user);
            bool isSuccess = false;
            if (result.Count > 0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get timepiece not evaluate success" : "Don't have timepiece not evaluate";

            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        //public async Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByCategory(int categoryId)
        //{
        //    var result = await _timepieceRepository.GetTimepieceByCategory(categoryId);
        //    bool isSuccess = false;
        //    if (result.Count > 0)
        //    {
        //        isSuccess = true;
        //    }
        //    else
        //    {
        //        isSuccess = false;
        //    }
        //    var message = isSuccess ? "Get timepiece success" : "Don't have timepiece";

        //    return new APIResponse<List<TimepieceViewModel>>
        //    {
        //        Message = message,
        //        isSuccess = isSuccess,
        //        Data = result
        //    };
        //}
        public async Task<APIResponse<List<TimepieceViewModel>>> GetProductByNameAndCategory(string name, int categoryId, User user)
        {
            var result = await _timepieceRepository.GetProductByNameAndCategory(name, categoryId, user);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all timepiece success" : "Don't have any timepiece";
            return new APIResponse<List<TimepieceViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        // CUD
        public async Task<APIResponse<Timepiece>> DeleteTimepiece(int id)
        {
            var timepiece = await _timepieceRepository.GetOneTimepiece(id);
            if (timepiece == null)
                return new APIResponse<Timepiece>
                {
                    Message = "Timepiece not exists",
                    isSuccess = false,
                    Data = timepiece
                };
            timepiece.IsDel = true;
            var result = _timepieceRepository.Update(timepiece);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Delete timepiece success" : "Delete timepiece fail";

            return new APIResponse<Timepiece>
            {
                Message = message,
                isSuccess = isSuccess
            };
        }
        public async Task<APIResponse<TimepieceViewModel>> UpdateTimepiece(int id, TimepieceViewModel timePiece)
        {
            var timepiece = await _timepieceRepository.GetTimepieceById(id);
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
            var result = _timepieceRepository.Update(timepiece.timepiece);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Update timepiece success" : "Update timepiece fail";

            return new APIResponse<TimepieceViewModel>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = timepiece
            };
        }
        public async Task<APIResponse<Timepiece>> UploadNewTimepiece(Timepiece timepiece)
        {
            var result = await _timepieceRepository.UploadNewTimepiece(timepiece);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Upload timepiece success" : "Upload timepiece fail";

            return new APIResponse<Timepiece>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        public async Task<APIResponse<Timepiece>> UpdateTimepiecePrice(int timpieceId, int price)
        {
            var result = await _timepieceRepository.UpdateTimepiecePrice(timpieceId, price);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Update timepiece price success" : "Update timepiece price fail";

            return new APIResponse<Timepiece>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        public async Task<APIResponse<string>> UpdateTimepieceOrder(List<OrdersDetail> ordersDetails, bool isOrder)
        {
            await _timepieceRepository.UpdateTimepieceIsOrder(ordersDetails, isOrder);
            return await Task.FromResult(new APIResponse<string>
            {
                Message = "Success",
                isSuccess = true,
            });
        }

    }
}
