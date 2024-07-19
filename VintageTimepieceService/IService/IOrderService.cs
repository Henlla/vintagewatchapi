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
        public Task<APIResponse<Order>> CreateOrder(Order order);
        public Task<APIResponse<List<Object>>> GetOrderOfUser(int userId);
    }
}
