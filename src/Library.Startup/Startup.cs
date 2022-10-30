using Library.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Startup
{
    public class Startup
    {
        public static void Configure(
            ConfigurationManager configuration,
            IServiceCollection services
        ) { }

        private static void ConfigureInjector(IServiceCollection services) { }

        private static void ConfigureDatabase(
            ConfigurationManager configuration,
            IServiceCollection services
        )
        {
            string connectionString = configuration["Database:ConnectionString"];

            services.AddDbContext<LibraryContext>(
                options =>
                    options.UseSqlite(
                        connectionString,
                        b => b.MigrationsAssembly("Library.Database")
                    )
            );
        }
    }
}
