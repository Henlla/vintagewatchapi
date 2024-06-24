using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public List<Order> GetAllTheOrderOfUser(int userId);
        public Order PostOrder(Order order);
    }
}
