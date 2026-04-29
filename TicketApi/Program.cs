using Application.Interfaces;
using Application.UseCases.Events.Handlers;
using Application.UsesCases.Reservations.Commands;
using Application.UsesCases.Reservations.Handlers;
using Application.UsesCases.Seats.Handlers;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5501")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});





var connectionString = builder.Configuration["ConnectionString"];
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString)); from mathias

////////////////////////////////////////////////////////////////////////////////// from jonathan la paz 
builder.Services.AddScoped<ISeatRepository, SeatRepository>();                  //
builder.Services.AddScoped<IGetSeatByIdHandler, GetSeatByIdHandler>();          //
builder.Services.AddScoped<IGetAllSeatsHandler, GetAllSeatsHandler>();          // inteccion dependencias 
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();    //       
builder.Services.AddScoped<IReserveSeatCommand, ReserveSeatCommand>();          //
builder.Services.AddScoped<IReserveSeatHandler, ReserveSeatHandler>();          //
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IGetEventSeatsHandler, GetEventSeatsHandler>();
builder.Services.AddScoped<IGetEventsHandler, GetEventsHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));/// cambio base de datos

var app = builder.Build();






using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();



    if (!context.Users.Any())
    {
        var user = new User
        {
            Name = "Test User",
            Email = "test@test.com",
            PasswordHash = "1234"
        };

        context.Users.Add(user);
        context.SaveChanges();
    }

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
                        Status = SeatStatus.Available,
                        Version = 0
                    });
                }
            }
        }

        context.SaveChanges();
    }
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
