using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class ReportsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW vReportsList
as
SELECT r.Id,
       r.InventoryId,
       b.[Name] as BuildingName,
       l.RoomDescription,
       r.CreatedAt as ReportDate
FROM Reports r
    INNER JOIN Inventories inv
        ON inv.Id = r.InventoryId
    INNER JOIN dict.Locations l
        ON inv.LocationId = l.Id
    INNER JOIN dict.Buildings b
        ON l.BuildingId = b.Id");

            migrationBuilder.Sql(@"CREATE OR ALTER VIEW vFileList
as
SELECT r.[Data],
       r.Id
FROM Reports r
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
