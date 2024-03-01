using Bookkeepify.Data;
using Bookkeepify.Models;
using Bookkeepify.Pages.Invoices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookkeepify.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;
        private readonly TransactionService _transactionService;
        private readonly TransactionTypeService _transactionTypeService;

        public PaymentService(AppDbContext context,
                                TransactionService transactionService,
                                TransactionTypeService transactionTypeService)
        {
            _context = context;
            _transactionService = transactionService;
            _transactionTypeService = transactionTypeService;
        }

        public async Task<List<Payment>> GetPaymentsAsync()
        {
            return await _context.Payments.Include(a => a.Customer).ToListAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<(bool success, string errorMessage)>
            SavePaymentAndTransactionAsync(Payment payment)
        {
            bool editMode = payment.Id > 0;
            Transaction transaction;
            List<TransactionDetail> tdetails = new List<TransactionDetail>();
            if (editMode)
            {
                transaction = await _transactionService.GetTransactionByIdAsync(payment.TransactionId);
            }
            else
            {
                transaction = new Transaction();
                transaction.Id = 0;
            }

            transaction.CustomerId = payment.CustomerId;
            transaction.Date = payment.Date;
            transaction.Description = $"Payment {payment.Customer.Name}";
            transaction.UserId = payment.UserId;
            transaction.TransactionTypeId = 3;
            var transactionType = await _transactionTypeService.GetTransactionTypeByIdAsync(transaction.TransactionTypeId);
            transaction.TransactionType = transactionType;

            tdetails.Add(new TransactionDetail
            {
                AccountId = (int)transaction.TransactionType.DebtAccountId,
                DebitAmount = payment.Amount,
                CreditAmount = 0
            });
            tdetails.Add(new TransactionDetail
            {
                AccountId = (int)transaction.TransactionType.CreditAccountId,
                DebitAmount = 0,
                CreditAmount = payment.Amount
            });

            var (tsuccess, terrMessage) = await _transactionService.SaveTransactionAndDetailAsync(transaction, tdetails);

            if (!tsuccess)
            {
                return (false, "Transaction Submit Error");
            }

            payment.TransactionId = transaction.Id;
            if (editMode)
            {
                _context.Entry(payment).State = EntityState.Modified;
            }
            else
            {
                _context.Payments.Add(payment);
            }

            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePaymentAndTransactionAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                int transactionId = payment.TransactionId;
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
                await _transactionService.DeleteTransactionAndDetailAsync(transactionId);
            }
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
