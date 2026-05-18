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
                // -------------------------
                // EVENT 1
                // -------------------------
                var hobbitDto = new CreateEventDto
                {
                    Name = "The Hobbit",
                    Date = new DateTime(2026, 6, 20, 22, 10, 0),
                    Place = "Disponible en tu Cinemacenter mas cercano",
                    Description = "Action",
                    State = "Available",
                    Url1 = "https://www.elcineenlasombra.com/wp-content/uploads/2014/12/the-hobbit-the-desolation-of-smaug-22982-2880x1800-copia.jpg",
                    Url2 = "",
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

                await createEventHandler.Handle(new CreateEventCommand(hobbitDto));

                // -------------------------
                // EVENT 2 - Regular Show 2
                // -------------------------
                var regularShowDto = new CreateEventDto
                {
                    Name = "Regular show 2",
                    Date = new DateTime(2026, 5, 28, 0, 33, 0),
                    Place = "cinemark",
                    Description = "una nueva aventura espera",
                    State = "activo",
                    Url1 = "https://wallpapers.com/images/hd/regular-show-1280-x-1024-background-km8fy0mdmx29tt2p.jpg",
                    Url2 = "",
                    Sectors = new List<CreateSectorDto>
        {
            new CreateSectorDto
            {
                Name = "s1",
                Rows = 20,
                Cols = 20,
                Price = 5,
                GridX = 9,
                GridY = 1
            },
            new CreateSectorDto
            {
                Name = "2",
                Rows = 20,
                Cols = 20,
                Price = 5000,
                GridX = 31,
                GridY = 1
            },
            new CreateSectorDto
            {
                Name = "3",
                Rows = 15,
                Cols = 42,
                Price = 3000,
                GridX = 9,
                GridY = 22
            }
        }
                };

                await createEventHandler.Handle(new CreateEventCommand(regularShowDto));

                // -------------------------
                // EVENT 3 - Hacker
                // -------------------------
                var hackerDto = new CreateEventDto
                {
                    Name = "Hacker",
                    Date = new DateTime(2026, 5, 28, 0, 33, 0),
                    Place = "cine Hoyts",
                    Description = "curso completo de hacking etico para ganar dolares",
                    State = "activo",
                    Url1 = "https://i.ytimg.com/vi/mHIMRJojDec/maxresdefault.jpg",
                    Url2 = "",
                    Sectors = new List<CreateSectorDto>
        {
            new CreateSectorDto { Name = "1", Rows = 10, Cols = 15, Price = 10, GridX = 14, GridY = 2 },
            new CreateSectorDto { Name = "2", Rows = 10, Cols = 15, Price = 10, GridX = 31, GridY = 2 },
            new CreateSectorDto { Name = "3", Rows = 15, Cols = 15, Price = 10, GridX = 14, GridY = 13 },
            new CreateSectorDto { Name = "4", Rows = 15, Cols = 15, Price = 10, GridX = 31, GridY = 13 },
            new CreateSectorDto { Name = "5", Rows = 15, Cols = 5, Price = 10, GridX = 8, GridY = 13 },
            new CreateSectorDto { Name = "6", Rows = 15, Cols = 5, Price = 10, GridX = 47, GridY = 13 }
        }
                };

                await createEventHandler.Handle(new CreateEventCommand(hackerDto));
            }
        }
    }
}
