﻿using InventoryAppAPI.DAL.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.Views
{
    public class InventoriedStockItemsViewConfiguration : IEntityTypeConfiguration<InventoriedStockItemView>
    {
        public void Configure(EntityTypeBuilder<InventoriedStockItemView> eb)
        {
            eb.HasKey(x => x.Id);
            eb.ToView("vInventoriedStockItemsList");
        }
    }
}
