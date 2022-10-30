namespace Library.Repository.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();

        Task DisposeAsync();
    }
}
