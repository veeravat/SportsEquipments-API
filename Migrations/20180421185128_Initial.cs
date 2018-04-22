using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OOAD.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    E_ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    E_amount = table.Column<int>(nullable: false),
                    E_name = table.Column<string>(nullable: true),
                    E_resv = table.Column<int>(nullable: false),
                    E_total = table.Column<int>(nullable: false),
                    E_used = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.E_ID);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentsRent",
                columns: table => new
                {
                    Rent_ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    E_ID = table.Column<int>(nullable: false),
                    Rent_by = table.Column<int>(nullable: false),
                    Rent_status = table.Column<string>(nullable: true),
                    Rent_time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentsRent", x => x.Rent_ID);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentsReserve",
                columns: table => new
                {
                    Resv_ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    E_ID = table.Column<int>(nullable: false),
                    Resv_by = table.Column<int>(nullable: false),
                    Resv_status = table.Column<string>(nullable: true),
                    Resv_time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentsReserve", x => x.Resv_ID);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentsReturn",
                columns: table => new
                {
                    Return_ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rent_ID = table.Column<int>(nullable: false),
                    Return_time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentsReturn", x => x.Return_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: true),
                    Faculty = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Rented = table.Column<int>(nullable: false),
                    Reserved = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    StudentId = table.Column<string>(nullable: true),
                    Telephon = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "EquipmentsRent");

            migrationBuilder.DropTable(
                name: "EquipmentsReserve");

            migrationBuilder.DropTable(
                name: "EquipmentsReturn");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
