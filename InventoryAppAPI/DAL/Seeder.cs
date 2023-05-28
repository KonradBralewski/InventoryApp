﻿using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace InventoryAppAPI.DAL
{
    public class Seeder
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public Seeder(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.UserRoles.Any())
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                    }

                    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    }
                }

                if (!_context.Users.Any())
                {
                    ApplicationUser admin = new()
                    {
                        Email = "Test_user@sggw.edu.pl",
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };

                    admin.EmailConfirmed = true;

                    await _userManager.CreateAsync(admin, "Sggw@sggw_PASSWORD1");

                    await _userManager.AddToRoleAsync(admin, UserRoles.Admin);
                }

                if (!_context.Buildings.Any())
                {
                    Building building = new Building
                    {
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "Admin",
                        ModifiedAt = DateTime.UtcNow,
                        ModifiedBy = "Admin",
                        Name = "Bulding 1",
                    };

                    _context.Buildings.Add(building);

                    await _context.SaveChangesAsync();
                }

                if(!_context.Locations.Any())
                {
                    Room room = new Room
                    {
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "Admin",
                        ModifiedAt = DateTime.UtcNow,
                        ModifiedBy = "Admin",
                        Name = "Room 1"
                    };

                    _context.Add(room);

                    await _context.SaveChangesAsync();

                    Location location = new Location
                    {
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "Admin",
                        ModifiedAt = DateTime.UtcNow,
                        ModifiedBy = "Admin",
                        BuildingId = 1,
                        RoomId = 1
                    };

                    _context.Add(location);

                    await _context.SaveChangesAsync();
                }

                if(!_context.Products.Any())
                {
                    Product product = new Product
                    {
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "Admin",
                        ModifiedAt = DateTime.UtcNow,
                        ModifiedBy = "Admin",
                        Name = "Myszka"

                    };

                    _context.Add(product);

                    await _context.SaveChangesAsync();
                }

                //if(!_context.StockItems.Any())
                //{
                //    StockItems stockItem = new StockItems
                //    {
                //        Code = "123456789",
                //        LocationId = 1,
                //        ProductId = 1,
                //        IsArchive = false,
                //        InventoriedAt = DateTime.UtcNow,
                //        InventoriedBy = "Admin"
                //    };

                //    _context.Add(stockItem);

                //    await _context.SaveChangesAsync();
                //}
                
            }
        }
    }

}
