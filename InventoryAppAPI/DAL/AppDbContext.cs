using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Base;
using InventoryAppAPI.DAL.Entities.Dicts;
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
        public DbSet<StockItems> StockItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Room> Rooms { get; set; }

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
            var entities = ChangeTracker.Entries().Where(
                x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email);

            var requestCaller = userEmail == null ? "Anonymous" : userEmail.Value;

            DateTime currentTime = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = currentTime;
                    ((BaseEntity)entity.Entity).CreatedBy = requestCaller;
                }

                ((BaseEntity)entity.Entity).ModifiedAt = currentTime;
                ((BaseEntity)entity.Entity).ModifiedBy = requestCaller;
            }
        }

    }

}
