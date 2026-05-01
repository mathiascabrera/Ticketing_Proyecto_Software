using Application.Interfaces;
using Application.UseCases.Events.Handlers;
using Application.UseCases.Events.Queries;
using Application.UseCases.Reservations.Commands;
using Application.UseCases.Reservations.Handlers;
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
builder.Services.AddScoped<IReserveSeatCommand, ReserveSeatCommand>();
builder.Services.AddScoped<IConfirmSeatCommand, ConfirmSeatCommand>();
builder.Services.AddScoped<IConfirmSeatHandler, ConfirmSeatHandler>();
builder.Services.AddScoped<IReserveSeatHandler, ReserveSeatHandler>();          //
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IGetEventSeatsHandler, GetEventSeatsHandler>();
builder.Services.AddScoped<IGetEventsHandler, GetEventsHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));/// cambio base de datos


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");


////////////////////////////////////////////////////////////////////////////////////////////////////

// donde estaba la precarga
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
