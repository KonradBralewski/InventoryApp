using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class scanningProcedureFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER procedure [dbo].[UP_APP_InventoryScanItemAdd]
    @UserId int,
    @LocationId int,
    @Code nvarchar(255),
    @isArchive bit
as
begin

    declare @InventoryId int = NULL,
            @Email nvarchar(256),
            @StockItemId int = NULL,
            @StockItemLocationId int = NULL,
            @ScannedItemId int = NULL, --- USED TO CHECK IF IT WAS SCANNED PREVIOUSLY
            @stockIsArchive bit = 0,   -- USED TO CHECK IT WAS ARCHIVED BEFORE SCANNING
            @Result bit


    begin tran ttt2

    select @ScannedItemId = [Id]
    from [dbo].[ScannedItems]
    where Code = @Code

    select @Email = [Email]
    from [dbo].[AspNetUsers]
    where Id = @UserId

    select @StockItemId = [Id],
           @stockIsArchive = [isArchive]
    from [dbo].[StockItems]
    where Code = @Code

    select @StockItemLocationId = LocationId
    from [dbo].[StockItems]
    where Code = @Code

    select @InventoryId = inv.id
    from [dbo].[Inventories] inv
        inner join [dbo].[InventoryStatus] st
            on inv.Id = st.InventoryId
               and st.StatusId = 1
               and st.IsActive = 1
    where inv.UserId = @UserId
          and inv.LocationId = @LocationId

    if @InventoryId is NULL
    begin
        rollback tran ttt2
        RAISERROR('Inwentaryzacja nie istnieje.', 11, 3)
        return
    end

    if @StockItemId is NULL
    begin
        rollback tran ttt2
        RAISERROR('Przedmiot o podanym kodzie nie istnieje.', 11, 3)
        return
    end

    if @stockIsArchive = 1
    begin
        rollback tran ttt2
        RAISERROR('Zeskanowy przedmiot jest w trakcie utylizacji.', 11, 3)
        return
    end

    if @ScannedItemId is not NULL
    begin
        rollback tran ttt2
        RAISERROR('Przedmiot był już zeskanowany.', 11, 3)
        return
    end

    if @StockItemLocationId != @LocationId
    begin

        DECLARE @ActualBuildingName nvarchar(256),
                @ActualRoomName nvarchar(256)

        SELECT @ActualBuildingName = b.[Name],
               @ActualRoomName = l.RoomDescription
        FROM [dbo].StockItems si
            INNER JOIN [dict].Locations l
                ON si.LocationId = si.LocationId
            INNER JOIN [dict].Buildings b
                ON b.Id = l.BuildingId
        WHERE si.Id = @StockItemId

        rollback tran ttt2

        RAISERROR(
                     'Przedmiot nie należy do tej lokalizacji. Powininen znajdować się w %s -> %s',
                     11,
                     3,
                     @ActualBuildingName,
                     @ActualRoomName
                 )
        return
    end




    INSERT INTO [dbo].[ScannedItems]
    (
        [inventoryId],
        [Code],
        [StockItemId],
        [isArchive],
        [InventoriedBy],
        [InventoriedAt]
    )
    select @InventoryId,
           @Code,
           @StockItemId,
           @isArchive,
           @Email,
           GETUTCDATE()


    if @StockItemId is not null
       and @StockItemLocationId = @LocationId
    begin
        set @Result = 1
    end
    else
    begin
        set @Result = 0
    end


    if @StockItemId is not null
       and @StockItemLocationId = @LocationId
       and @isArchive = 1
    begin
        UPDATE [dbo].[StockItems]
        set [IsArchive] = 1
        where Code = @Code

    end

    if @StockItemLocationId <> @LocationId
       and @isArchive = 1
    begin
        RAISERROR('Zapisano bez archiwizacji - błąd lokalizacji.', 11, 3)
        return
    end



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
