using System.Dynamic;
using Library.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Library.Database
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Book>()
                .Property(e => e.Authors)
                .HasConversion(
                    authors => JsonSerializer.Serialize(authors, (JsonSerializerOptions)null),
                    str =>
                        JsonSerializer.Deserialize<ExpandoObject>(str, (JsonSerializerOptions)null)
                );

            modelBuilder
                .Entity<EventStore>()
                .Property(e => e.Data)
                .HasConversion(
                    data => JsonSerializer.Serialize(data, (JsonSerializerOptions)null),
                    str =>
                        JsonSerializer.Deserialize<ExpandoObject>(str, (JsonSerializerOptions)null)
                );
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<EventStore> EventStore { get; set; }
    }
}
