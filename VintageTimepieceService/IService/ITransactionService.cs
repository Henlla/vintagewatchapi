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
        public Task<APIResponse<Transaction>> CreateTransaction(Transaction transaction);
        public Task<APIResponse<Transaction>> UpdateTransaction(Transaction transaction);
        public Task<APIResponse<List<Transaction>>> GetAllTransactions();
        public Task<APIResponse<List<Transaction>>> GetAllTransactionsOfUsers(User user);
    }
}
