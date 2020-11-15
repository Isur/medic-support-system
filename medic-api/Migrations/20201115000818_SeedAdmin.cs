using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace medic_api.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "firstname", "lastname", "password", "role", "username" },
                values: new object[] { new Guid("1e32f9b5-5a9c-4e65-8ee3-7cf22fb3b1ab"), "Admin", "Admin", "r6gZh6ISsCXAWQMr47hysUEEf+RiPESHdIcyGD4W3kPF62Uo", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("1e32f9b5-5a9c-4e65-8ee3-7cf22fb3b1ab"));
        }
    }
}
