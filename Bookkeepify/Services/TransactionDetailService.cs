using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookkeepify.Services
{
    public class TransactionDetailService
    {
        private readonly AppDbContext _context;

        public TransactionDetailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionDetail>> GetTransactionDetailsAsync(int transactionId)
        {
            return await _context.TransactionDetails
                .Include(td => td.Account) // Eager load the associated account
                .Include(td => td.TransactionMethod)
                .Where(td => td.TransactionId == transactionId)
                .ToListAsync();
        }
        public async Task AddTransactionDetailAsync(TransactionDetail transactionDetail)
        {
            _context.TransactionDetails.Add(transactionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionDetailAsync(int transactionDetailId)
        {
            var transactionDetail = await _context.TransactionDetails.FindAsync(transactionDetailId);
            if (transactionDetail != null)
            {
                _context.TransactionDetails.Remove(transactionDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteTransactionDetailsAsync(int transactionId)
        {
            try
            {
                var detailsToDelete = await _context.TransactionDetails
                    .Where(td => td.TransactionId == transactionId)
                    .ToListAsync();

                if (detailsToDelete != null && detailsToDelete.Any())
                {
                    _context.TransactionDetails.RemoveRange(detailsToDelete);
                    await _context.SaveChangesAsync();
                    return true; // Deletion successful
                }

                return false; // No details found for the given transactionId
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return false; // Deletion failed
            }
        }

        public async Task UpdateTransactionDetailAsync(TransactionDetail transactionDetail)
        {
            _context.Entry(transactionDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<(bool success, string errorMessage)> SaveTransactionDetailsAsync(List<TransactionDetail> details)
        {
            try
            {
                if (details == null || details.Count == 0)
                {
                    return (false, "No transaction details provided.");
                }

                decimal sumDebit = 0;
                decimal sumCredit = 0;

                foreach (var detail in details)
                {
                    sumDebit += detail.DebitAmount;
                    sumCredit += detail.CreditAmount;
                    detail.Id = 0;

                    if (detail.DebitAmount > 0)
                    {
                        detail.TransactionMethodId = 1; // Assuming 1 is for Debit
                    }
                    else if (detail.CreditAmount > 0)
                    {
                        detail.TransactionMethodId = 2; // Assuming 2 is for Credit
                    }
                }

                if (sumDebit != sumCredit || sumDebit <= 0)
                {
                    return (false, "Sum of DebitAmount must equal sum of CreditAmount and be greater than 0.");
                }

                foreach (var detail in details)
                {
                    _context.TransactionDetails.Add(detail);
                }

                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred while saving transaction details: {ex.Message}");
            }
        }

    }
}
