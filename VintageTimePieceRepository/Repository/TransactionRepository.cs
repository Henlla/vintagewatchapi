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
    public class TransactionRepository : BaseRepository<Transactions>, ITransactionRepository
    {
        public TransactionRepository(VintagedbContext context) : base(context)
        {
        }

        // R
        public async Task<List<Transactions>> GetAllTransactions()
        {
            var result = await FindAll().Where(trans => trans.IsDel == false).ToListAsync();
            return result;
        }
        public async Task<List<Transactions>> GetAllTransactionsOfUsers(User user)
        {
            var result = await _context.Transactions
                .Include(t => t.Order)
                .ThenInclude(o => o.User)
                .Where(t => t.Order.User == user && t.IsDel == false)
                .ToListAsync();
            return result;
        }
        public async Task<Transactions?> GetTransactionOfOrder(int orderId)
        {
            var result = await _context.Transactions
                        .Include(t => t.Order)
                        .Where(t => t.OrderId == orderId && t.IsDel == false)
                        .SingleOrDefaultAsync();
            return result;
        }

        // CUD
        public async Task<Transactions> CreateTransaction(Transactions transaction)
        {
            var result = await Add(transaction);
            return result;
        }
        public async Task<Transactions> UpdateTransaction(Transactions transaction)
        {
            var result = await Update(transaction);
            return result;
        }

        public async Task<Transactions> UpdateTransactionRefund(int orderId, int refundId)
        {
            var transaction = _context.Transactions.Where(trans => trans.OrderId == orderId).SingleOrDefault();
            transaction.RefundId = refundId;
            return await Update(transaction);
        }
    }
}
