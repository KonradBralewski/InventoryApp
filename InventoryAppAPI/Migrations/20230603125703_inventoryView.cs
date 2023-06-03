using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class inventoryView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create or alter view vInventoryList
as
select i.id as InventoryId,
       i.LocationId as LocationId,
       i.UserId as UserId,
       i.[Description] as [Description],
       invs.IsActive as IsActive,
       invStatusCode.StatusName as StatusName
from Inventories i
    join InventoryStatus invs
        on invs.InventoryId = i.Id
    join dict.InventoryStatus invStatusCode
        on invs.StatusId = invStatusCode.Id

go

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
