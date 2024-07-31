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
            var result = await _orderRepository.PostOrder(order);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create order success" : "Create order fail";

            return new APIResponse<Order>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        public async Task<APIResponse<List<OrderViewModel>>> GetAllOrder()
        {
            var result = await _orderRepository.GetAllOrder();
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all order success" : "Don't have any order";

            return new APIResponse<List<OrderViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Order>> GetOrderById(int orderId)
        {
            var result = await _orderRepository.GetOrderById(orderId);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get order success" : "Get order fail";
            return new APIResponse<Order>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<List<OrderViewModel>>> GetOrderOfUser(string status, User user)
        {
            var result = await _orderRepository.GetAllOrderOfUser(status, user);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get order of user success" : "Don't have any order";
            return new APIResponse<List<OrderViewModel>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Order>> UpdateOrderStatus(int orderId, string status)
        {
            var result = await _orderRepository.UpdateOrderStatus(orderId, status);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Update order status success" : "Update order status fail";
            return new APIResponse<Order>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
