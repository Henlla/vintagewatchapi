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
            var result = await _orderDetailRepository.PostOrderDetail(orderDetail);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create order detail success" : "Create order detail fail";

            return new APIResponse<OrdersDetail>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
