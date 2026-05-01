using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { 1, "test@test.com", "Test User", "1234" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
