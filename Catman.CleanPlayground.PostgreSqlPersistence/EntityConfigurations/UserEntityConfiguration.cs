namespace Catman.CleanPlayground.PostgreSqlPersistence.EntityConfigurations
{
    using Catman.CleanPlayground.PostgreSqlPersistence.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder
                .Property(user => user.Id)
                .HasColumnName("id");

            builder
                .Property(user => user.Username)
                .HasColumnName("username")
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(user => user.DisplayName)
                .HasColumnName("display_name")
                .HasMaxLength(40)
                .IsRequired();

            builder.OwnsOne(user => user.Password, ConfigurePassword);
            builder.Navigation(user => user.Password).IsRequired();
        }

        private static void ConfigurePassword(OwnedNavigationBuilder<UserEntity, UserPassword> builder)
        {
            builder
                .Property(password => password.Hash)
                .HasColumnName("password_hash")
                .HasMaxLength(60)
                .IsFixedLength()
                .IsRequired();

            builder
                .Property(password => password.Salt)
                .HasColumnName("password_salt")
                .HasMaxLength(29)
                .IsFixedLength()
                .IsRequired();
        }
    }
}
