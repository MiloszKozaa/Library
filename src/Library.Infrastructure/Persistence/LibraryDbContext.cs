using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence
{
    internal class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        //public DbSet<Book> Books { get; set; }
        //public DbSet<Borrow> Borrows { get; set; }
        //public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
