using System.Dynamic;
using Library.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Library.Database
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<EventStore>()
                .Property(e => e.Data)
                .HasConversion(
                    data => JsonConvert.SerializeObject(data),
                    str => JsonConvert.DeserializeObject<ExpandoObject>(str)
                );
        }

        public DbSet<EventStore> EventStore { get; set; }
    }
}
