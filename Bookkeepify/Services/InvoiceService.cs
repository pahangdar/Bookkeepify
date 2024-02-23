using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookkeepify.Services
{
    public class InvoiceService
    {
        private readonly AppDbContext _context;
        private Invoice _invoice;

        public Invoice Invoice
        {
            get { return _invoice; }
            set { _invoice = value; }
        }

        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetInvoicesAsync(InvoiceType? invoiceType)
        {
            if (invoiceType == null)
            {
                return await _context.Invoices
                                     .Include(a => a.Customer)
                                     .ToListAsync();
            }
            else
            {
                return await _context.Invoices
                                     .Include(a => a.Customer)
                                     .Where(a => a.InvoiceType == invoiceType)
                                     .ToListAsync();
            }
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
