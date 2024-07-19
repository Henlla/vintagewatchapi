using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<APIResponse<OrdersDetail>> CreateOrderDetail(OrdersDetail orderDetail)
        {
            var result = await Task.FromResult(_orderDetailRepository.PostOrderDetail(orderDetail));
            if (result == null)
            {
                return new APIResponse<OrdersDetail>()
                {
                    Message = "Create order detail fail",
                    isSuccess = false,
                    Data = result
                };
            }
            return new APIResponse<OrdersDetail>
            {
                Message = "Create order detail success",
                isSuccess = true,
                Data = result
            };
        }
    }
}
