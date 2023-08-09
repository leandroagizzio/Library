using CoreLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowableBook> BorrowableBooks { get; set; }
        public DbSet<QueueBorrowBook> QueueBorrowBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

    }
}
