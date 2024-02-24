using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookkeepify.Services
{
    public class InvoiceService
    {
        private readonly AppDbContext _context;
        private readonly InvoiceDetailService _invoiceDetailService;
        private Invoice _invoice;

        public Invoice Invoice
        {
            get { return _invoice; }
            set { _invoice = value; }
        }

        public InvoiceService(AppDbContext context, InvoiceDetailService invoiceDetailService)
        {
            _context = context;
            _invoiceDetailService = invoiceDetailService;
        }

        public async Task<List<Invoice>> GetInvoicesAsync(InvoiceType? invoiceType)
        {
            if (invoiceType == null)
            {
                return await _context.Invoices
                                     .Include(a => a.Customer)
                                     .Include(a => a.InvoiceDetails)
                                     .ToListAsync();
            }
            else
            {
                return await _context.Invoices
                                     .Include(a => a.Customer)
                                     .Include(a => a.InvoiceDetails)
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

        public async Task<(bool success, string errorMessage)>
            SaveInvoiceAndDetailsAsync(Invoice invoice, List<InvoiceDetail> details)
        {
            bool editMode = invoice.Id > 0;
            if (editMode)
            {
                await UpdateInvoiceAsync(invoice);
            }
            else
            {
                await AddInvoiceAsync(invoice);
            }
            int invoiceId = invoice.Id; ;
            // Assign the obtained invoice ID to the InvoiceId property of each InvoiceDetail
            // Save the invoice details
            if (editMode)
            {
                await _invoiceDetailService.DeleteInvoiceDetailsAsync(invoiceId);
            }
            foreach (var detail in details)
            {
                detail.InvoiceId = invoiceId;
                detail.Id = 0;
            }
            var (success, errMessage) = await _invoiceDetailService.SaveInvoiceDetailsAsync(details);
            return (success, errMessage);
        }
        public async Task DeleteInvoiceAndDetailAsync(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice != null)
            {
                await _invoiceDetailService.DeleteInvoiceDetailsAsync(invoice.Id);
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

    }
}
