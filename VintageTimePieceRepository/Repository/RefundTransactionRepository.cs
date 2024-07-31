using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class RefundTransactionRepository : BaseRepository<RefundTransaction>, IRefundTransactionRepository
    {
        public RefundTransactionRepository(VintagedbContext context) : base(context)
        {
        }

        public async Task<RefundTransaction> CreateRefundTransaction(RefundTransaction refundTransaction)
        {
            return await Add(refundTransaction);
        }
    }
}
