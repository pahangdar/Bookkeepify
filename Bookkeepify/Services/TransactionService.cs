using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookkeepify.Services
{
    public class TransactionService
    {
        private readonly AppDbContext _context;
        private Transaction _transaction;

        public Transaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transactions
                                 .Include(a => a.TransactionType)
                                 .Include(a => a.Customer)
                                 .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
