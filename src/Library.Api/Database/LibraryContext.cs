using Library.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Database
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<EventStore> EventStore { get; set; }
    }
}