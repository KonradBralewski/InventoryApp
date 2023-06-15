using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class CleanupScannedItemsProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER procedure [dbo].[UP_APP_CleanupScannedItems]
as
begin


    begin try
        begin tran ttt2
        declare @Result bit

        DELETE sci
        FROM ScannedItems scI
            INNER JOIN Inventories inv
                ON scI.InventoryId = inv.Id
            INNER JOIN InventoryStatus invStatus
                ON invStatus.InventoryId = inv.Id
        WHERE invStatus.StatusId = 2

        set @Result = 1


        commit tran ttt2

    end try
    begin catch
        SELECT ERROR_MESSAGE() AS ErrorMessage;
        IF XACT_STATE() <> 0
        begin
            rollback tran ttt2
        end


        set @Result = 0


    end catch

    select @Result as 'Result'

end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
