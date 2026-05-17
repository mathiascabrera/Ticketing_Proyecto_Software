using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UsesCases.Events.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();

            var userManager = services.GetRequiredService<UserManager<User>>();

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var createEventHandler =
                services.GetRequiredService<ICreateEventHandler>();

            // -------------------------
            // ROLES
            // -------------------------

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(
                    new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(
                    new IdentityRole("User"));
            }

            // -------------------------
            // ADMIN
            // -------------------------

            var adminEmail = "admin@test.com";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new User
                {
                    UserName = "admin",
                    Email = adminEmail
                };

                var result = await userManager.CreateAsync(
                    admin,
                    "Admin123!"
                );

                if (!result.Succeeded)
                {
                    var errors = string.Join(" | ",
                        result.Errors.Select(e => e.Description));

                    throw new Exception(errors);
                }

                await userManager.AddToRoleAsync(
                    admin,
                    "Admin"
                );
            }

            // -------------------------
            // USER
            // -------------------------

            var userEmail = "user@test.com";

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new User
                {
                    UserName = "user",
                    Email = userEmail
                };

                await userManager.CreateAsync(
                    user,
                    "User123"
                );

                await userManager.AddToRoleAsync(
                    user,
                    "User"
                );
            }

            // -------------------------
            // EVENT                              ACA DIJISMOS , YA TENEMOS ALGO QUE NOS CONSTRUYE EL EVENTO ... USEMOS EL HANDLER 
            // -------------------------

            bool eventExists =
                await context.Events.AnyAsync();

            if (!eventExists)
            {
                var dto = new CreateEventDto
                {
                    Name = "The Hobbit",
                    Date = new DateTime(2026, 6, 20, 22, 10, 0),
                    Place = "Disponible en tu Cinemacenter mas cercano",
                    Description = "Action",
                    State = "Available",

                    Url1 = "https://www.elcineenlasombra.com/wp-content/uploads/2014/12/the-hobbit-the-desolation-of-smaug-22982-2880x1800-copia.jpg",
                    Url2 = "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/6ba42b80-1619-4ed4-b250-0f0718fd3141/f6b2b5af2d4217fca21c52e6b286f67bd78c2d79.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=500",

                    Sectors = new List<CreateSectorDto>
                {
                    new CreateSectorDto
                    {
                        Name = "vip",
                        Rows = 10,
                        Cols = 30,
                        Price = 5000,
                        GridX = 15,
                        GridY = 0
                    },

                    new CreateSectorDto
                    {
                        Name = "general",
                        Rows = 30,
                        Cols = 40,
                        Price = 2500,
                        GridX = 10,
                        GridY = 11
                    }
                }
                };

                await createEventHandler.Handle( new CreateEventCommand(dto) );
            }
        }
    }
}
