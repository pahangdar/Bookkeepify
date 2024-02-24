using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;


namespace Bookkeepify.Services
{
    public class TransactionService
    {
        private readonly AppDbContext _context;
        private Transaction _transaction;
        private readonly TransactionDetailService _transactionDetailService;

        public Transaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        public TransactionService(AppDbContext context, TransactionDetailService transactionDetailService)
        {
            _context = context;
            _transactionDetailService = transactionDetailService;
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

        public async Task<(bool success, string errorMessage)>
            SaveTransactionAndDetailAsync(Transaction transaction, List<TransactionDetail> details)
        {
            bool editMode = transaction.Id > 0;
            if (editMode)
            {
                await UpdateTransactionAsync(transaction);
            }
            else
            {
                await AddTransactionAsync(transaction);
            }
            int transactionId = transaction.Id; ;
            // Assign the obtained transaction ID to the TransactionId property of each TransactionDetail
            foreach (var detail in details)
            {
                detail.TransactionId = transactionId;
            }
            // Save the transaction details
            if (editMode)
            {
                await _transactionDetailService.DeleteTransactionDetailsAsync(transactionId);
            }
            var (success, errMessage) = await _transactionDetailService.SaveTransactionDetailsAsync(details);
            return(success, errMessage);
        }

        public async Task DeleteTransactionAndDetailAsync(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                await _transactionDetailService.DeleteTransactionDetailsAsync(transaction.Id);
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

    }
}
