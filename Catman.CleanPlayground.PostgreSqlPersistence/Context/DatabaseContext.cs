namespace Catman.CleanPlayground.PostgreSqlPersistence.Context
{
    using System.Reflection;
    using Catman.CleanPlayground.PostgreSqlPersistence.Entities;
    using Microsoft.EntityFrameworkCore;

    internal class DatabaseContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
