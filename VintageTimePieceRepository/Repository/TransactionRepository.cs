﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(VintagedbContext context) : base(context)
        {
        }

        // R
        public async Task<List<Transaction>> GetAllTransactions()
        {
            var result = await FindAll().Where(trans => trans.IsDel == false).ToListAsync();
            return result;
        }
        public async Task<List<Transaction>> GetAllTransactionsOfUsers(User user)
        {
            var result = await _context.Transactions
                .Include(t => t.Order)
                .ThenInclude(o => o.User)
                .Where(t => t.Order.User == user && t.IsDel == false)
                .ToListAsync();
            return result;
        }
        public async Task<Transaction?> GetTransactionOfOrder(Order order)
        {
            var result = await _context.Transactions
                        .Include(t => t.Order)
                        .Where(t => t.Order == order && t.IsDel == false)
                        .SingleOrDefaultAsync();
            return result;
        }

        // CUD
        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            var result = await Add(transaction);
            return result;
        }
        public async Task<Transaction> UpdateTransaction(Transaction transaction)
        {
            var result = await Update(transaction);
            return result;
        }
    }
}
