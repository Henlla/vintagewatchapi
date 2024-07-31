using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IRefundTransactionRepository : IBaseRepository<RefundTransaction>
    {
        public Task<RefundTransaction> CreateRefundTransaction(RefundTransaction refundTransaction);
    }
}
