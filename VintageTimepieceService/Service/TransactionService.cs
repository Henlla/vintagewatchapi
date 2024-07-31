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

        // R
        public async Task<APIResponse<List<Transactions>>> GetAllTransactions()
        {
            var result = await _transactionRepository.GetAllTransactions();
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all transaction success" : "Don't have any transaction";
            return new APIResponse<List<Transactions>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        public async Task<APIResponse<List<Transactions>>> GetAllTransactionsOfUsers(User user)
        {
            var result = await _transactionRepository.GetAllTransactionsOfUsers(user);
            bool isSuccess = true;
            if (!result.Any())
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all transaction success" : "Don't have any transaction";
            return new APIResponse<List<Transactions>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        public async Task<APIResponse<Transactions>> GetTransactionOfOrder(int orderId)
        {
            var result = await _transactionRepository.GetTransactionOfOrder(orderId);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }

            var message = isSuccess ? "Get transaction success" : "Get transaction fail";
            return new APIResponse<Transactions>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        // CUD
        public async Task<APIResponse<Transactions>> CreateTransaction(Transactions transaction)
        {
            var result = await _transactionRepository.CreateTransaction(transaction);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create transaction success" : "Create transaction fail";
            return new APIResponse<Transactions>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
        //public async Task<APIResponse<Transactions>> UpdateTransaction(Transactions transaction)
        //{
        //    var result = await _transactionRepository.UpdateTransaction(transaction);
        //    bool isSuccess = true;
        //    if (result == null)
        //    {
        //        isSuccess = false;
        //    }
        //    var message = isSuccess ? "Update transaction success" : "Update transaction fail";
        //    return new APIResponse<Transactions>
        //    {
        //        Message = message,
        //        isSuccess = isSuccess,
        //        Data = result
        //    };
        //}

        public async Task<APIResponse<Transactions>> UpdateTransactionRefund(int orderId, int refundId)
        {
            var result = await _transactionRepository.UpdateTransactionRefund(orderId, refundId);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess |= false;
            }
            var message = isSuccess ? "Update transaction success" : "Update transaction fail";
            return new APIResponse<Transactions>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
