using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class vInventoriedStockItemsList_ScanningProcedureFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW vInventoriedStockItemsList
AS
select si.Id,
       si.LocationId,
       si.Code,
       p.[Name],
       sci.Id as ScannedItemEntryId,
       si.IsArchive
FROM StockItems si
    JOIN dict.Products p
        on si.ProductId = p.Id
    LEFT JOIN ScannedItems sci
        ON sci.StockItemId = si.Id");

            migrationBuilder.Sql(@"USE [InventoryAppDb]
GO
/****** Object:  StoredProcedure [dbo].[UP_APP_InventoryScanItemAdd]    Script Date: 06.06.2023 19:27:46 ******/
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
            @Result bit




    begin tran ttt2

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
        RAISERROR('Inwentaryzacja nie istnieje', 11, 3)
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
        RAISERROR('Saved but not archived. Location error', 11, 3)
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
