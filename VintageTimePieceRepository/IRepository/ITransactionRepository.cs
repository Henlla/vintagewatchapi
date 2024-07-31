using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface ITransactionRepository : IBaseRepository<Transactions>
    {
        // R
        public Task<List<Transactions>> GetAllTransactions();
        public Task<List<Transactions>> GetAllTransactionsOfUsers(User user);
        public Task<Transactions?> GetTransactionOfOrder(int orderId);

        // CUD
        public Task<Transactions> CreateTransaction(Transactions transaction);
        public Task<Transactions> UpdateTransaction(Transactions transaction);
        public Task<Transactions> UpdateTransactionRefund(int orderId,int refundId);
    }
}
