namespace Catman.CleanPlayground.Application.Persistence.UnitOfWork
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Repositories;

    public interface IUnitOfWork : IAsyncDisposable
    {
        IUserRepository Users { get; }

        Task SaveAsync();
    }
}
