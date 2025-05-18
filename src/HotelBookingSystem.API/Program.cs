using HotelBookingSystem.Application.Interfaces;
using HotelBookingSystem.Application.Mapping;
using HotelBookingSystem.Infrastructure.Persistence;
using HotelBookingSystem.Infrastructure.Repositories;
using HotelBookingSystem.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddDbContext<HotelBookingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IHotelBookingSeeder, HotelBookingSeeder>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IHotelBookingSeeder>();
    await seeder.SeedAsync();
}


app.Run();

public partial class Program { }