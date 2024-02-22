using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookkeepify.Services
{
    public class InvoiceDetailService
    {
        private readonly AppDbContext _context;

        public InvoiceDetailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDetail>> GetInvoiceDetailsAsync(int invoiceId)
        {
            return await _context.InvoiceDetails
                .Include(td => td.Product) // Eager load the associated account
                .Where(td => td.InvoiceId == invoiceId)
                .ToListAsync();
        }
        public async Task AddInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            _context.InvoiceDetails.Add(invoiceDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceDetailAsync(int invoiceDetailId)
        {
            var invoiceDetail = await _context.InvoiceDetails.FindAsync(invoiceDetailId);
            if (invoiceDetail != null)
            {
                _context.InvoiceDetails.Remove(invoiceDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteInvoiceDetailsAsync(int invoiceId)
        {
            try
            {
                var detailsToDelete = await _context.InvoiceDetails
                    .Where(td => td.InvoiceId == invoiceId)
                    .ToListAsync();

                if (detailsToDelete != null && detailsToDelete.Any())
                {
                    _context.InvoiceDetails.RemoveRange(detailsToDelete);
                    await _context.SaveChangesAsync();
                    return true; // Deletion successful
                }

                return false; // No details found for the given invoiceId
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return false; // Deletion failed
            }
        }

        public async Task UpdateInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            _context.Entry(invoiceDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<(bool success, string errorMessage)> SaveInvoiceDetailsAsync(List<InvoiceDetail> details)
        {
            try
            {
                if (details == null || details.Count == 0)
                {
                    return (false, "No invoice details provided.");
                }

                foreach (var detail in details)
                {

                }

                //if (sumDebit != sumCredit || sumDebit <= 0)
                //{
                //    return (false, "Sum of DebitAmount must equal sum of CreditAmount and be greater than 0.");
               // }

                foreach (var detail in details)
                {
                    _context.InvoiceDetails.Add(detail);
                }

                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred while saving invoice details: {ex.Message}");
            }
        }

    }
}
