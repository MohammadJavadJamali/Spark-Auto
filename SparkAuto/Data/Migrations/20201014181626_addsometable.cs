using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkAuto.Data.Migrations
{
    public partial class addsometable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "serviceHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miles = table.Column<double>(nullable: false),
                    TotalOrice = table.Column<double>(nullable: false),
                    DataAdded = table.Column<DateTime>(nullable: false),
                    CarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_serviceHeaders_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_serviceShoppingCarts_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_serviceShoppingCarts_serviceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "serviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceHeaderId = table.Column<int>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: false),
                    ServicePrice = table.Column<double>(nullable: false),
                    ServiceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_serviceDetails_serviceHeaders_ServiceHeaderId",
                        column: x => x.ServiceHeaderId,
                        principalTable: "serviceHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_serviceDetails_serviceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "serviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_serviceDetails_ServiceHeaderId",
                table: "serviceDetails",
                column: "ServiceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_serviceDetails_ServiceTypeId",
                table: "serviceDetails",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_serviceHeaders_CarId",
                table: "serviceHeaders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_serviceShoppingCarts_CarId",
                table: "serviceShoppingCarts",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_serviceShoppingCarts_ServiceTypeId",
                table: "serviceShoppingCarts",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serviceDetails");

            migrationBuilder.DropTable(
                name: "serviceShoppingCarts");

            migrationBuilder.DropTable(
                name: "serviceHeaders");
        }
    }
}
