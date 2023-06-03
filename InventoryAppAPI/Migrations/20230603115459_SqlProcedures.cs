using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class SqlProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE or ALTER procedure [dbo].[UP_APP_InventoryAdd]
    @LocationId int,
    @UserId nvarchar(450),
    @Descr nvarchar(max)
as
begin


    begin try

        begin tran ttt2

        declare @Result bit
        -- body ---

        declare @userName nvarchar(100)
        select @userName = UserName
        from AspNetUsers
        where Id = @UserId

        INSERT INTO dbo.Inventories
        VALUES
        (@LocationId, @UserId, 'Inwentaryzacja', @userName, SYSDATETIME())
        declare @InventoryId int
        select @InventoryId = SCOPE_IDENTITY()
        INSERT INTO dbo.InventoryStatus
        VALUES
        (1, @InventoryId, 1, @UserId, SYSDATETIME())


        set @Result = 1
        return 0
        commit tran ttt2

    end try
    begin catch

        IF XACT_STATE() <> 0
        begin
            rollback tran ttt2
        end

        set @Result = 0
        return -1


    end catch

    select @Result as 'Result'


end

GO

CREATE or ALTER procedure [dbo].[UP_APP_InventoryScanItemAdd]
    @UserId nvarchar(450),
    @LocationId int,
    @Code nvarchar(255),
    @isArchive bit
as
begin

    declare @InventoryId int = NULL,
            @UserName nvarchar(256),
            @StockItemId int = NULL,
            @StockItemLocationId int = NULL,
            @Result bit


    begin try

        begin tran ttt2

        select @UserName = [UserName]
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

            RAISERROR('Inwentaryzacja nie istnieje', 21, 37)


            commit tran ttt2

        end

        -- body ---



        INSERT INTO [dbo].[ScannedItems]
        (
            [inventoryId],
            [Code],
            [StockItemId],
            [isArchive],
            [InventoriedBy]
        )
        select @InventoryId,
               @Code,
               @StockItemId,
               @isArchive,
               @UserName


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
            RAISERROR('Not archived', 11, 3)
        end



        commit tran ttt2
        return 0

    end try
    begin catch

        IF XACT_STATE() <> 0
        begin
            rollback tran ttt2
        end

        set @Result = 0
        return -1


    end catch

    select @Result as 'Result'

end

GO

CREATE or ALTER procedure [dbo].[UP_APP_InventoryStatusSet]
    @InventoryId int,
    @StatusId int,
    @UserId nvarchar(450)
as
begin


    begin try

        begin tran ttt2
        declare @Result bit

        -- body ---
        declare @userName nvarchar(100)
        select @userName = UserName
        from AspNetUsers
        where Id = @UserId

        UPDATE dbo.InventoryStatus
        SET IsActive = 0
        WHERE @InventoryId = InventoryId
        INSERT INTO dbo.InventoryStatus
        VALUES
        (@StatusId, @InventoryId, 1, @userName, SYSDATETIME())


        set @Result = 1


        commit tran ttt2

    end try
    begin catch

        IF XACT_STATE() <> 0
        begin
            rollback tran ttt2
        end


        set @Result = 0


    end catch

    select @Result as 'Result'

end

GO
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
      
        }
    }
}
