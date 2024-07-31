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
    public class RefundTransactionService : IRefundTransactionService
    {
        private IRefundTransactionRepository _refundTransactionRepository;
        public RefundTransactionService(IRefundTransactionRepository refundTransactionRepository)
        {
            _refundTransactionRepository = refundTransactionRepository;
        }
        public async Task<APIResponse<RefundTransaction>> CreateRefundTransaction(RefundTransaction refundTransaction)
        {
            var result = await _refundTransactionRepository.CreateRefundTransaction(refundTransaction);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create refund transaction success" : "Create refund transaction fail";
            return new APIResponse<RefundTransaction>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
