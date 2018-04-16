using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OOAD.Migrations
{
    public partial class AddEquipmentDBSet : Migration
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
                    E_total = table.Column<int>(nullable: false)
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
                    Rent_time = table.Column<string>(nullable: true)
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
                    Resv_time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentsReserve", x => x.Resv_ID);
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
        }
    }
}
