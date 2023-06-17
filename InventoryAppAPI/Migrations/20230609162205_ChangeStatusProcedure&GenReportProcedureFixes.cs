using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    public partial class ChangeStatusProcedureGenReportProcedureFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"USE [InventoryAppDb]
GO
/****** Object:  StoredProcedure [dbo].[UP_APP_InventoryStatusSet]    Script Date: 09.06.2023 18:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[UP_APP_InventoryStatusSet]
    @InventoryId int,
    @StatusId int,
    @UserId int
as
begin


    begin try
        begin tran ttt2
        declare @Result bit

        -- body ---
        declare @email nvarchar(100)
        select @email = [Email]
        from [dbo].[AspNetUsers]
        where Id = @UserId

        UPDATE dbo.InventoryStatus
        SET [IsActive] = 0,
            [StatusId] = @StatusId,
            [ModifiedAt] = GETUTCDATE(),
            [ModifiedBy] = @email
        WHERE [InventoryId] = @InventoryId


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

end
");
            migrationBuilder.Sql(@"USE InventoryAppDb
GO

ALTER procedure [dbo].[UP_APP_GenerateRap] @inventoryId int
as
begin



    declare @invLocation int

    select @invLocation = LocationId
    from dbo.Inventories
    where Id = @inventoryId

    if exists (select * from dbo.Inventories where Id = @inventoryId)
    begin

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
    )              then
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
    )              then
                           0
                       else
                           1
                   end as [isTarget],
                   CASE
                       WHEN s1.isArchive = 1 or s2.isArchive = 1 THEN
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
    end
    else
    begin
        raiserror('Podana inwentaryzacja nie istnieje', 11, 3)
    end

end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
