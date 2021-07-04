namespace Catman.CleanPlayground.PostgreSqlPersistence.Migrations
{
    using System;
    using Catman.CleanPlayground.PostgreSqlPersistence.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
    
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210704194106_AddUsers")]
    internal partial class AddUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Catman.CleanPlayground.PostgreSqlPersistence.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("display_name");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Catman.CleanPlayground.PostgreSqlPersistence.Entities.UserEntity", b =>
                {
                    b.OwnsOne("Catman.CleanPlayground.PostgreSqlPersistence.Entities.UserPassword", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserEntityId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("character(60)")
                                .HasColumnName("password_hash")
                                .IsFixedLength(true);

                            b1.Property<string>("Salt")
                                .IsRequired()
                                .HasMaxLength(29)
                                .HasColumnType("character(29)")
                                .HasColumnName("password_salt")
                                .IsFixedLength(true);

                            b1.HasKey("UserEntityId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityId");
                        });

                    b.Navigation("Password")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
