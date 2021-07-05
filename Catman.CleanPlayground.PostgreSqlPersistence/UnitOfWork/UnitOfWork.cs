namespace Catman.CleanPlayground.PostgreSqlPersistence.UnitOfWork
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Repositories;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.PostgreSqlPersistence.Context;
    using Catman.CleanPlayground.PostgreSqlPersistence.Repositories;

    internal class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }

        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            Users = new UserRepository(context);

            _context = context;
        }
        
        public Task SaveAsync() =>
            _context.SaveChangesAsync();
        
        public ValueTask DisposeAsync() =>
            _context.DisposeAsync();
    }
}
