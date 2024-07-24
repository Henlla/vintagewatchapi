using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class OrderDetailRepository : BaseRepository<OrdersDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(VintagedbContext context) : base(context)
        {
        }

        public async Task<List<OrdersDetail>> GetOrderDetailOfOrder(int orderId)
        {
            var result = await FindAll().Where(od => od.OrderId == orderId && od.IsDel == false).ToListAsync();
            return result;
        }

        public async Task<OrdersDetail> PostOrderDetail(OrdersDetail orderDetail)
        {
            var result = await Add(orderDetail);
            return result;
        }
    }
}
