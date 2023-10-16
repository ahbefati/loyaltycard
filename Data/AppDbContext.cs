using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;

namespace loyaltycard.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchAddress> BranchAddresses { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Sale> Sales{ get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }

    }
}
