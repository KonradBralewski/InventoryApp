﻿using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Base;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.Models.Responses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace InventoryAppAPI.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<InventoryStatus> InventoryStatus { get; set; }
        public DbSet<InventoryStatusDict> InventoryStatusDict { get; set; }

        public DbSet<InventoryDTO> InventoryView { get; set; }

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
