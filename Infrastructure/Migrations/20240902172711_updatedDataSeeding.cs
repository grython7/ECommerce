using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("19559900-08d1-460a-b46b-08dcca12a94e"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CreatedOn", "Email", "IsAdmin", "Name", "PasswordHash", "PasswordSalt", "Phone", "Status", "UpdatedOn" },
                values: new object[] { new Guid("3ab4b46a-8d10-41ce-d001-08dccb73f215"), "123 bla bla st.", new DateTime(2024, 9, 1, 2, 14, 54, 0, DateTimeKind.Unspecified), "admin@ldc.com", true, "admin", "Cvpqm17FCPsRjAdKdJTitnyNu2isY88GFVMfOLDiLKA=", "xLLO/PUZthdu0xjc/YNY1w==", "01234567890", "Active", new DateTime(2024, 9, 1, 2, 14, 54, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("3ab4b46a-8d10-41ce-d001-08dccb73f215"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CreatedOn", "Email", "IsAdmin", "Name", "PasswordHash", "PasswordSalt", "Phone", "Status", "UpdatedOn" },
                values: new object[] { new Guid("19559900-08d1-460a-b46b-08dcca12a94e"), "123 bla bla st.", new DateTime(2024, 9, 1, 2, 14, 54, 0, DateTimeKind.Unspecified), "admin@ldc.com", true, "admin", "wM+SX1pv7uMsQmr7V5oyfi4vqHDNPnkdWZ7Xe/xjp5U=", "6KkbPhZg4gD+NFCaEQDuew==", "01234567890", "Active", new DateTime(2024, 9, 1, 2, 14, 54, 0, DateTimeKind.Unspecified) });
        }
    }
}
