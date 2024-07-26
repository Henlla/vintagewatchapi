using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        // R
        public Task<List<Transaction>> GetAllTransactions();
        public Task<List<Transaction>> GetAllTransactionsOfUsers(User user);
        public Task<Transaction?> GetTransactionOfOrder(Order order);

        // CUD
        public Task<Transaction> CreateTransaction(Transaction transaction);
        public Task<Transaction> UpdateTransaction(Transaction transaction);
    }
}
