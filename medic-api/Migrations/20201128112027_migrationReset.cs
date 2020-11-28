using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace medic_api.Migrations
{
    public partial class migrationReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    firstname = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medicaldata",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    pregnancies = table.Column<int>(nullable: false),
                    glucose = table.Column<int>(nullable: false),
                    bloodpressure = table.Column<int>(nullable: false),
                    skinthickness = table.Column<int>(nullable: false),
                    insulin = table.Column<int>(nullable: false),
                    diabetespedigreefunction = table.Column<double>(nullable: false),
                    bmi = table.Column<double>(nullable: false),
                    age = table.Column<int>(nullable: false),
                    prediction = table.Column<bool>(nullable: true),
                    result = table.Column<bool>(nullable: true),
                    userid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicaldata", x => x.id);
                    table.ForeignKey(
                        name: "FK_medicaldata_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "firstname", "lastname", "password", "role", "username" },
                values: new object[] { new Guid("2440bd90-d285-4b5b-b6e8-f8808c101763"), "yFdWByrf/mq61BKxY6tsxc7eqkFU0ytljU9G6yyet2S1i/UV4X8+bQq7TntEHe3CBgBGzuBiXi0GaWeGDMTeTou0+YN2cQYlt5xh6qHhREAHK1UHWcfaqySpUhz4bCRBDxbY3KBuNr3dn2E/GDc61fn8tQjm4emE33e9utwoECU1BqrJ0yxa4/vjd8lPVoQJlnvi7XWFJ9tC2aJbQmDgNOEdlBQ8w72XNkednD9T5r5ugmVT8JJ3YS+Xc0d16tqMMqHXmHAk3nJAJW5xULLFlK6OkMpJVXjLiP/ieUDS5Y/a3p4TWj8C2f3fPh1N/gHq5/gk9uCGfdHTPPtC2jlBEA==", "aBGT9HXVi/8Q7GtmPX+aQbimBRjQl8vaCtNmh0MlT3BuEvewZ7DkJfgFz00XUFb4t36PWZg+w+i0dW8/BgMyGjOMTWsH+w0KDww72G2hPf/6iT5bKooDRi0IK7nf0Lj0YV0PgfChmiwzzld/X6xmKSp7LR4HMC+j6VBXvtpM6Vl0OTteftD4cf70WBd+CU0TJJ1+Qq+Ww7sN10RJ/dnTfyUpHuvxnl36MB9/2kYdZWvwdnTaV+mrvtJP3ZvR8niqOqoijomm+tKH/CS8+t+J66dyfeaAIdYFRkrKkw0xjqjc29tcnXep4OdBevfvu8hGwDNBvFvPbEjYtjbXrwEGdA==", "vFW18BRQhMgpyPbKegfXukkcTBL0Xoi4BSVPU2MqIAFsJ7ZP", "Admin", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_medicaldata_userid",
                table: "medicaldata",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medicaldata");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
