using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<APIResponse<Transaction>> CreateTransaction(Transaction transaction)
        {
            var result = await _transactionRepository.CreateTransaction(transaction);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create transaction success" : "Create transaction fail";
            return new APIResponse<Transaction>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<List<Transaction>>> GetAllTransactions()
        {
            var result = await _transactionRepository.GetAllTransactions();
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all transaction success" : "Don't have any transaction";
            return new APIResponse<List<Transaction>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<List<Transaction>>> GetAllTransactionsOfUsers(User user)
        {
            var result = await _transactionRepository.GetAllTransactionsOfUsers(user);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all transaction success" : "Don't have any transaction";
            return new APIResponse<List<Transaction>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Transaction>> UpdateTransaction(Transaction transaction)
        {
            var result = await _transactionRepository.UpdateTransaction(transaction);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Update transaction success" : "Update transaction fail";
            return new APIResponse<Transaction>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
