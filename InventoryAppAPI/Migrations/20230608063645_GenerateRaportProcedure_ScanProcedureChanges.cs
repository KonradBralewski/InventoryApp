using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class GenerateRaportProcedure_ScanProcedureChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE procedure [dbo].[UP_APP_GenerateRap] @inventoryId int
as
begin



    declare @invLocation int

    select @invLocation = LocationId
    from dbo.Inventories
    where Id = @inventoryId

    select ProductName,
           Code,
           isScanned,
           isInStock,
           isTarget,
           isArchived
    from
    (
        SELECT case
                   when
                   (
                       select p.[Name] from dict.Products p where p.Id = s2.ProductId
                   ) is null then
                       ISNULL(
                       (
                           select p.[Name]
                           from dict.Products p,
                                StockItems s3
                           where p.Id = s3.ProductId
                                 and s3.Code = s1.Code
                       ),
                       ''
                             )
                   else
               (
                   select p.[Name] from dict.Products p where p.Id = s2.ProductId
               )
               end as [ProductName],
               COALESCE(s1.Code, s2.Code) AS Code,
               CASE
                   WHEN s1.id IS NOT NULL THEN
                       1
                   ELSE
                       0
               END AS [isScanned],
               CASE
                   WHEN s2.id IS NOT NULL THEN
                       1
                   WHEN exists
                        (
                            select 1
                            from StockItems s2a
                            where s2a.Code = s1.Code
                                  and s2a.LocationId <> @invLocation
                        ) then
                       1
                   ELSE
                       0
               END AS [isInStock],
               case
                   when exists
                        (
                            select 1
                            from StockItems s2a
                            where s2a.Code = s1.Code
                                  and s2a.LocationId <> @invLocation
                        ) then
                       0
                   else
                       1
               end as [isTarget],
               CASE
                   WHEN s1.isArchive = 1 THEN
                       1
                   ELSE
                       0
               END AS [isArchived],
               s2.LocationId
        FROM ScannedItems s1
            FULL OUTER JOIN StockItems s2
                ON s1.Code = s2.Code
                   and (
                           s1.inventoryId = @inventoryId
                           or s1.inventoryId is null
                       )
                   and (
                           s2.LocationId = @invLocation
                           or s2.LocationId is NULL
                       )
    ) as alias
    where (
              alias.LocationId = @invLocation
              or alias.LocationId is NULL
          )


end");

            migrationBuilder.Sql(@"USE [InventoryAppDb]
GO
/****** Object:  StoredProcedure [dbo].[UP_APP_InventoryScanItemAdd]    Script Date: 08.06.2023 08:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[UP_APP_InventoryScanItemAdd]
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
            @Result bit


    begin tran ttt2

    select @ScannedItemId = [Id]
    from [dbo].[ScannedItems]
    where Code = @Code

    select @Email = [Email]
    from [dbo].[AspNetUsers]
    where Id = @UserId

    select @StockItemId = [Id]
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
