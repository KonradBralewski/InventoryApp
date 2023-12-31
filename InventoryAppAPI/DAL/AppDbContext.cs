﻿using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Base;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Procedures;
using InventoryAppAPI.DAL.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace InventoryAppAPI.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        // TO DO -> ADD ALL REMAINING
        public DbSet<Product> Products { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<InventoryStatus> InventoryStatus { get; set; }
        public DbSet<InventoryStatusDict> InventoryStatusDict { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Report> Reports { get; set; }

        // NOT MAPPED SECTION
        public DbSet<InventoriedStockItemView> InventoriedStockItemsView { get; set; }
        public DbSet<InventoryView> InventoryView { get; set; }
        public DbSet<GenerateReportProcedure> RawReports { get; set; }
        public DbSet<ReportView> ReportsView { get; set; }
        public DbSet<FileView> Files { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override int SaveChanges()
        {
            AutoCreatedOrModifiedProperites();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AutoCreatedOrModifiedProperites();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AutoCreatedOrModifiedProperites()
        {
            if(_httpContextAccessor.HttpContext == null) { return; }

            var entities = ChangeTracker.Entries().Where(
                x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email);

            var requestCaller = userEmail == null ? "Anonymous" : userEmail.Value;

            DateTime currentTime = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                ((BaseEntity)entity.Entity).ModifiedAt = currentTime;
                ((BaseEntity)entity.Entity).ModifiedBy = requestCaller;
            }
        }

    }

}
