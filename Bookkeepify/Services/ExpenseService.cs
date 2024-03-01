using Bookkeepify.Data;
using Bookkeepify.Models;
using Bookkeepify.Pages.Invoices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookkeepify.Services
{
    public class ExpenseService
    {
        private readonly AppDbContext _context;
        private readonly TransactionService _transactionService;
        private readonly TransactionTypeService _transactionTypeService;

        public ExpenseService(AppDbContext context,
                                TransactionService transactionService,
                                TransactionTypeService transactionTypeService)
        {
            _context = context;
            _transactionService = transactionService;
            _transactionTypeService = transactionTypeService;
        }

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<(bool success, string errorMessage)>
            SaveExpenseAndTransactionAsync(Expense expense)
        {
            bool editMode = expense.Id > 0;
            Transaction transaction;
            List<TransactionDetail> tdetails = new List<TransactionDetail>();
            if (editMode)
            {
                transaction = await _transactionService.GetTransactionByIdAsync(expense.TransactionId);
            }
            else
            {
                transaction = new Transaction();
                transaction.Id = 0;
            }

            transaction.Date = expense.Date;
            transaction.Description = $"Expense {expense.Description}";
            transaction.UserId = expense.UserId;
            transaction.TransactionTypeId = 2;
            var transactionType = await _transactionTypeService.GetTransactionTypeByIdAsync(transaction.TransactionTypeId);
            transaction.TransactionType = transactionType;

            tdetails.Add(new TransactionDetail
            {
                AccountId = (int)transaction.TransactionType.DebtAccountId,
                DebitAmount = expense.Amount,
                CreditAmount = 0
            });
            tdetails.Add(new TransactionDetail
            {
                AccountId = (int)transaction.TransactionType.CreditAccountId,
                DebitAmount = 0,
                CreditAmount = expense.Amount
            });

            var (tsuccess, terrMessage) = await _transactionService.SaveTransactionAndDetailAsync(transaction, tdetails);

            if (!tsuccess)
            {
                return (false, "Transaction Submit Error");
            }

            expense.TransactionId = transaction.Id;
            if (editMode)
            {
                _context.Entry(expense).State = EntityState.Modified;
            }
            else
            {
                _context.Expenses.Add(expense);
            }

            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task DeleteExpenseAsync(int expenseId)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteExpenseAndTransactionAsync(int expenseId)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense != null)
            {
                int transactionId = expense.TransactionId;
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
                await _transactionService.DeleteTransactionAndDetailAsync(transactionId);
            }
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
