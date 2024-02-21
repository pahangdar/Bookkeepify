using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookkeepify.Services
{
    public class AccountTypeService
    {
        private readonly AppDbContext _context;

        public AccountTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountType>> GetAccountTypesAsync()
        {
            return await _context.AccountTypes.ToListAsync();
        }

        public async Task AddAccountTypeAsync(AccountType accountType)
        {
            _context.AccountTypes.Add(accountType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountTypeAsync(int accountTypeId)
        {
            var accountType = await _context.AccountTypes.FindAsync(accountTypeId);
            if (accountType != null)
            {
                _context.AccountTypes.Remove(accountType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAccountTypeAsync(AccountType accountType)
        {
            _context.Entry(accountType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
