using AutoMapper;
using Library.Database;
using Library.Repository.Core.Interfaces;

namespace Library.Repository.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly LibraryContext context;

        public UnitOfWork(LibraryContext context, IMapper mapper)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await this.context.DisposeAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
