using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Task<List<OrderViewModel>> GetAllOrderOfUser(string status, User user);
        public Task<List<OrderViewModel>> GetAllOrder();
        public Task<Order?> GetOrderById(int orderId);
        public Task<Order?> UpdateOrderStatus(int orderId, string status);
        public Task<Order> PostOrder(Order order);
    }
}
