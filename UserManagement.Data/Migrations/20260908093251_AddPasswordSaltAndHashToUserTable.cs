using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordSaltAndHashToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");
        }
    }
}
