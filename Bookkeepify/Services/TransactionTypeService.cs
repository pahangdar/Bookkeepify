using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookkeepify.Services
{
    public class TransactionTypeService
    {
        private readonly AppDbContext _context;

        public TransactionTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionType>> GetTransactionTypesAsync()
        {
            return await _context.TransactionTypes
                                 .Include(t => t.DebtAccount)
                                 .Include(t => t.CreditAccount)
                                 .ToListAsync();
        }

        public async Task<TransactionType> GetTransactionTypeByIdAsync(int transactionTypeId)
        {
            return await _context.TransactionTypes.FindAsync(transactionTypeId);
        }

        public async Task AddTransactionTypeAsync(TransactionType transactionType)
        {
            _context.TransactionTypes.Add(transactionType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionTypeAsync(int transactionTypeId)
        {
            var transactionType = await _context.TransactionTypes.FindAsync(transactionTypeId);
            if (transactionType != null)
            {
                _context.TransactionTypes.Remove(transactionType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTransactionTypeAsync(TransactionType transactionType)
        {
            _context.Entry(transactionType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
