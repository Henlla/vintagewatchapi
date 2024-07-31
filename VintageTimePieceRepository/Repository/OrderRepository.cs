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
                              join trans in _context.Transactions on order.OrderId equals trans.OrderId
                              join ordDetail in _context.OrdersDetails on order.OrderId equals ordDetail.OrderId into listOrderDetail
                              where order.IsDel == false
                              select new OrderViewModel
                              {
                                  Order = order,
                                  OrderDetail = listOrderDetail.ToList(),
                                  Transaction = trans
                              }).ToListAsync();
            return data;
        }
        public async Task<List<OrderViewModel>> GetAllOrderOfUser(string status,User user)
        {
            var data = await (from order in _context.Orders
                              join trans in _context.Transactions on order.OrderId equals trans.OrderId into transGroup
                              from trans in transGroup.DefaultIfEmpty()
                              join transRefund in _context.RefundTransactions on trans.RefundId equals transRefund.RefundId into refundGroup
                              from transRefund in refundGroup.DefaultIfEmpty()
                              join orderDetail in _context.OrdersDetails on order.OrderId equals orderDetail.OrderId into listOrder
                              where order.IsDel == false && order.User == user
                              && (order.Status.Equals(status.ToLower()) || status == "all")
                              orderby order.OrderId descending
                              select new OrderViewModel
                              {
                                  Order = order,
                                  Transaction = trans,
                                  RefundTransaction = transRefund,
                                  TimeRemining =  DateTime.Parse(order.OrderDate.Value.ToString()) > DateTime.Now.AddHours(-1),
                                  OrderDetail = listOrder.ToList(),
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
