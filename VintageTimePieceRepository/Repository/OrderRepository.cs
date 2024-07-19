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

        public List<Object> GetAllTheOrderOfUser(int userId)
        {
            var data = (from order in _context.Orders
                        join orderDetail in _context.OrdersDetails on order.OrderId equals orderDetail.OrderId into listOrder
                        where order.IsDel == false
                        select new 
                        {
                            order = order,
                            ordersDetails = listOrder.ToList()
                        }).ToList();
            return data.Cast<Object>().ToList();
        }

        public Order PostOrder(Order order)
        {
            var result = Add(order);
            return result;
        }
    }
}
