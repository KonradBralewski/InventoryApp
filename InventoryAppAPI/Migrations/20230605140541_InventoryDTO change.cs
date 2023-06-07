using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class InventoryDTOchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create or alter view vInventoryList
as
select i.id as InventoryId,
       b.Id as BuildingId,
       i.LocationId as LocationId,
       b.[Name] as BuildingName,
       l.RoomDescription as RoomDescription,
       i.UserId as UserId,
       i.[Description] as [Description],
       invs.IsActive as IsActive,
       invStatusCode.StatusName as StatusName
from Inventories i
    join InventoryStatus invs
        on invs.InventoryId = i.Id
    join dict.InventoryStatus invStatusCode
        on invs.StatusId = invStatusCode.Id
    join dict.Locations l
        on l.Id = i.LocationId
    join dict.Buildings b
        on b.Id = l.BuildingId

go");

       
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
