using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace medic_api.Migrations
{
    public partial class DatabaseStructure : Migration
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
