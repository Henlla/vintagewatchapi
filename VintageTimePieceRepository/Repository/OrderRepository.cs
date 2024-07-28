using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(VintagedbContext context) : base(context)
        {
        }
        // R
        public async Task<List<OrderViewModel>> GetAllOrder()
        {
            var data = await (from order in _context.Orders
                              join ordDetail in _context.OrdersDetails on order.OrderId equals ordDetail.OrderId into listOrderDetail
                              where order.IsDel == false
                              select new OrderViewModel
                              {
                                  order = order,
                                  orderDetail = listOrderDetail.ToList()
                              }).ToListAsync();
            return data;
        }
        public async Task<List<OrderViewModel>> GetAllOrderOfUser(User user)
        {
            var data = await (from order in _context.Orders
                              join orderDetail in _context.OrdersDetails on order.OrderId equals orderDetail.OrderId into listOrder
                              where order.IsDel == false
                              && order.User == user
                              select new OrderViewModel
                              {
                                  order = order,
                                  orderDetail = listOrder.ToList()
                              }).ToListAsync();
            return data;
        }
        public async Task<Order?> GetOrderById(int orderId)
        {
            var result = await _context.Orders
                .Where(or => or.OrderId == orderId && or.IsDel == false)
                .SingleOrDefaultAsync();
            return result;
        }

        // CUD
        public async Task<Order> PostOrder(Order order)
        {
            var result = await Add(order);
            return result;
        }
        public async Task<Order?> UpdateOrderStatus(int orderId, string status)
        {
            var currentOrder = await _context.Orders.SingleOrDefaultAsync(ord => ord.OrderId == orderId && ord.IsDel == false);
            if (currentOrder == null)
            {
                return currentOrder;
            }

            currentOrder.Status = status;
            var result = await Update(currentOrder);
            return result;
        }
    }
}
