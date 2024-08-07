﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IOrderService
    {
        // R
        public Task<APIResponse<List<OrderViewModel>>> GetOrderOfUser(string status, User user);
        public Task<APIResponse<List<OrderViewModel>>> GetAllOrder();
        public Task<APIResponse<Order>> GetOrderById(int orderId);
        // CUD
        public Task<APIResponse<Order>> CreateOrder(Order order);
        public Task<APIResponse<Order>> UpdateOrderStatus(int orderId, string status);
    }
}
