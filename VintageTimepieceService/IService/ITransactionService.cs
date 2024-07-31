using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface ITransactionService
    {
        // R
        public Task<APIResponse<List<Transactions>>> GetAllTransactions();
        public Task<APIResponse<List<Transactions>>> GetAllTransactionsOfUsers(User user);
        public Task<APIResponse<Transactions>> GetTransactionOfOrder(int orderId);
        // CUD
        public Task<APIResponse<Transactions>> CreateTransaction(Transactions transaction);
        //public Task<APIResponse<Transactions>> UpdateTransaction(Transactions transaction);
        public Task<APIResponse<Transactions>> UpdateTransactionRefund(int orderId, int refundId);
    }
}
