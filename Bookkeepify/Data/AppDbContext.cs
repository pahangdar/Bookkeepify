using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookkeepify.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("LocalConnection"));
        }

        public DbSet<Menu>? Menus { get; set; }
        public DbSet<MenuPermission>? Permissions { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TransactionMethod> TransactionMethods { get; set; }
        public DbSet<TransactionType> TransactionTypes  { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
