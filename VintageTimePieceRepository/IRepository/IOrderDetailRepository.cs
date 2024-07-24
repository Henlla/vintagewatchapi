using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IOrderDetailRepository : IBaseRepository<OrdersDetail>
    {
        public Task<OrdersDetail> PostOrderDetail(OrdersDetail orderDetail);
        public Task<List<OrdersDetail>> GetOrderDetailOfOrder(int orderId);
    }
}
