using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class ReportsFileStreamTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_InventoryId",
                table: "Reports",
                column: "InventoryId");

            migrationBuilder.Sql(@"alter table [dbo].[Reports] add [RowId] uniqueidentifier rowguidcol not null");
            migrationBuilder.Sql(@"alter table [dbo].[Reports] add constraint [UQ_Reports_RowId] UNIQUE NONCLUSTERED ([RowId])");
            migrationBuilder.Sql(@"alter table [dbo].[Reports] add constraint [DF_Reports_RowId] default (newid()) for [RowId]");
            migrationBuilder.Sql(@"alter table [dbo].[Reports] add [Data] varbinary(max) FILESTREAM not null");
            migrationBuilder.Sql(@"alter table [dbo].[Reports] add constraint [DF_Reports_Data] default(0x) for [Data]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
