namespace Catman.CleanPlayground.PostgreSqlPersistence.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    
    internal partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    password_hash = table.Column<string>(type: "character(60)", fixedLength: true, maxLength: 60, nullable: false),
                    password_salt = table.Column<string>(type: "character(29)", fixedLength: true, maxLength: 29, nullable: false),
                    display_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
