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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<APIResponse<Order>> CreateOrder(Order order)
        {
            var result = await Task.FromResult(_orderRepository.PostOrder(order));
            if (result != null)
            {
                return new APIResponse<Order>()
                {
                    Message = "Create order success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<Order>
            {
                Message = "Create order fail",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<List<Object>>> GetOrderOfUser(int userId)
        {
            var result = await Task.FromResult(_orderRepository.GetAllTheOrderOfUser(userId));
            return new APIResponse<List<Object>>
            {
                Message = "Get order of user success",
                isSuccess = true,
                Data = result
            };
        }
    }
}
