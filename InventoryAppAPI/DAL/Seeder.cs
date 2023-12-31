﻿using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace InventoryAppAPI.DAL
{
    public class Seeder
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public Seeder(AppDbContext context, RoleManager<IdentityRole<int>> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
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
                        await _roleManager.CreateAsync(new IdentityRole<int>(UserRoles.User));
                    }

                    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));
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
            }
        }
    }

}
