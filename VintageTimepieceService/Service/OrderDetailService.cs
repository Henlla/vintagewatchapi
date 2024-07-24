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

        public async Task<APIResponse<List<OrdersDetail>>> GetAllOrderDetailOfOrder(int orderId)
        {
            var result = await _orderDetailRepository.GetOrderDetailOfOrder(orderId);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all order detail success" : "Don't have any order detail";
            return new APIResponse<List<OrdersDetail>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
