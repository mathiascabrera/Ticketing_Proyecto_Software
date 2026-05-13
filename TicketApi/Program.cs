using System.Text;
using Application.Interfaces;
using Application.UsesCases.Events.Handlers;
using Application.UsesCases.Reservations.Commands;
using Application.UsesCases.Reservations.Handlers;
using Application.UsesCases.Seats.Handlers;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// =========================
// CONTROLLERS + SWAGGER
// =========================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<JwtService>();




var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);




// =========================
// CORS (UNO SOLO)
// =========================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("WWW-Authenticate");
    });
});


// =========================
// DB CONTEXT
// =========================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// =========================
// IDENTITY
// =========================
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


// =========================
// JWT
// =========================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});




builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});














// =========================
// DEPENDENCY INJECTION
// =========================
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IGetSeatByIdHandler, GetSeatByIdHandler>();
builder.Services.AddScoped<IGetAllSeatsHandler, GetAllSeatsHandler>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReserveSeatsCommand, ReserveSeatsCommand>();
builder.Services.AddScoped<IConfirmSeatCommand, ConfirmSeatCommand>();
builder.Services.AddScoped<IConfirmSeatHandler, ConfirmSeatHandler>();
builder.Services.AddScoped<IReserveSeatsHandler, ReserveSeatsHandler>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IGetEventSeatsHandler, GetEventSeatsHandler>();
builder.Services.AddScoped<IGetEventsHandler, GetEventsHandler>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<AuthService>();

builder.Services.AddHostedService<ReservationExpirationService>();

// =========================
// BUILD APP
// =========================
var app = builder.Build();


// =========================
// PIPELINE
// =========================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// =========================
// SEED DATA (CORRECTO)
// =========================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    Console.WriteLine(userManager == null);

    // EVENTS SEED
    if (!context.Events.Any())
    {
        var evento = new Event
        {
            Name = "Concierto Rock",
            EventDate = DateTime.UtcNow.AddMonths(1),
            Venue = "Estadio Central",
            Status = "Active"
        };

        context.Events.Add(evento);
        context.SaveChanges();

        var sectorA = new Sector
        {
            Name = "Platea A",
            Price = 10000,
            Capacity = 50,
            EventId = evento.Id
        };

        var sectorB = new Sector
        {
            Name = "Platea B",
            Price = 8000,
            Capacity = 50,
            EventId = evento.Id
        };

        context.Sectors.AddRange(sectorA, sectorB);
        context.SaveChanges();

        foreach (var sector in new[] { sectorA, sectorB })
        {
            for (char row = 'A'; row <= 'E'; row++)
            {
                for (int num = 1; num <= 10; num++)
                {
                    context.Seats.Add(new Seat
                    {
                        Id = Guid.NewGuid(),
                        SectorId = sector.Id,
                        RowIdentifier = row.ToString(),
                        SeatNumber = num,
                        Status = SeatStatus.Available
                    });
                }
            }
        }

        context.SaveChanges();
    }

    using (var rolescope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }


    using (var adminScope = app.Services.CreateScope())
    {
        var adminUserManager = adminScope.ServiceProvider
            .GetRequiredService<UserManager<User>>();

        var adminRoleManager = adminScope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        // crear rol Admin si no existe
        if (!await adminRoleManager.RoleExistsAsync("Admin"))
        {
            await adminRoleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // buscar admin
        var adminUser = await adminUserManager.FindByEmailAsync("admin@admin.com");

        // si no existe, crearlo
        if (adminUser == null)
        {
            var user = new User
            {
                UserName = "Admin",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };

            var result = await adminUserManager.CreateAsync(user, "admin123");

            if (result.Succeeded)
            {
                await adminUserManager.AddToRoleAsync(user, "Admin");
            }
        }
    }


}

app.Run();