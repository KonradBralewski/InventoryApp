using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class validInventoryAddProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER procedure [dbo].[UP_APP_InventoryAdd]
    @LocationId int,
    @UserId int,
    @Descr nvarchar(max)
as
begin

    begin tran ttt2

    declare @Result bit
    declare @userName nvarchar(100)
    select @userName = UserName
    from AspNetUsers
    where Id = @UserId
    -- body ---

    IF NOT EXISTS
    (
        SELECT *
        FROM dbo.InventoryStatus as ii
            inner join dbo.Inventories as i
                on ii.InventoryId = i.Id
        WHERE i.UserId = @UserId
              and ii.IsActive = 1
              and ii.StatusId = 1
    )
    begin

        INSERT INTO dbo.Inventories
        VALUES
        (@LocationId, @UserId, 'Inwentaryzacja', @userName, SYSDATETIME())
        declare @InventoryId int
        select @InventoryId = SCOPE_IDENTITY()
        INSERT INTO dbo.InventoryStatus
        VALUES
        (1, @InventoryId, 1, @UserId, SYSDATETIME())


        set @Result = 1
    END
    else
    BEGIN
        set @Result = 0
        rollback tran ttt2
        RAISERROR('Użytkownik posiada już rozpoczętą inwentaryzację', 11, 3)
        return
    END


    commit tran ttt2


    select @Result as 'Result'


end
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
