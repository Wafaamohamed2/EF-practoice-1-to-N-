using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_practoice_1_to_N_.Migrations
{
    /// <inheritdoc />
    public partial class CarsRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePalate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Moke = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.UniqueConstraint("AK_Cars_LicensePalate_State", x => new { x.LicensePalate, x.State });
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    RecordOfSafeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSold = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarState = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.RecordOfSafeId);
                    table.ForeignKey(
                        name: "FK_Records_Cars_CarLicensePlate_CarState",
                        columns: x => new { x.CarLicensePlate, x.CarState },
                        principalTable: "Cars",
                        principalColumns: new[] { "LicensePalate", "State" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_CarLicensePlate_CarState",
                table: "Records",
                columns: new[] { "CarLicensePlate", "CarState" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
