﻿using Library.Database;
using Library.Repository.Core.Interfaces;
using Library.Repository.Repositories;
using Library.Repository.Repositories.Interfaces;

namespace Library.Repository.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly LibraryContext context;

        public IEventStoreRepository EventStore { get; private set; }

        public UnitOfWork(LibraryContext context)
        {
            this.context = context;

            this.EventStore = new EventStoreRepository(context);
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
