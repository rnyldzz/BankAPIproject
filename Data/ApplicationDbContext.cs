using BankAPIproject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPIproject.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Bu, veritabanı bağlantı bilgilerini alacak olan yapıcı metot (constructor)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Bunlar veritabanındaki tablolara karşılık gelen özellikler
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
