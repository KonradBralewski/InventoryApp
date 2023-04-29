using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dict");

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_Entity_Id",
                        column: x => x.Id,
                        principalTable: "Entity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Entity_Id",
                        column: x => x.Id,
                        principalTable: "Entity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Entity_Id",
                        column: x => x.Id,
                        principalTable: "Entity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "dict",
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locations_Entity_Id",
                        column: x => x.Id,
                        principalTable: "Entity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "dict",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsArchive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    InventoriedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoriedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockItems_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "dict",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dict",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_BuildingId",
                schema: "dict",
                table: "Locations",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RoomId",
                schema: "dict",
                table: "Locations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_LocationId",
                table: "StockItems",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_ProductId",
                table: "StockItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockItems");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "dict");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "dict");

            migrationBuilder.DropTable(
                name: "Buildings",
                schema: "dict");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "dict");

            migrationBuilder.DropTable(
                name: "Entity");
        }
    }
}
