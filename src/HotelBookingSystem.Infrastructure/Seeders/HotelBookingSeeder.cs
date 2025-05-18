using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Persistence;

namespace HotelBookingSystem.Infrastructure.Seeders
{
    public class HotelBookingSeeder : IHotelBookingSeeder
    {
        private readonly HotelBookingDbContext _dbContext;

        public HotelBookingSeeder(HotelBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
           
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }

           
            if (await _dbContext.Database.CanConnectAsync())
            {
                
                if (!_dbContext.Hotels.Any())
                {
                    var hotels = GetHotels();
                    _dbContext.Hotels.AddRange(hotels);
                    await _dbContext.SaveChangesAsync();
                }

             
                if (!_dbContext.Rooms.Any())
                {
                    var rooms = GetRooms();
                    _dbContext.Rooms.AddRange(rooms);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Bookings.Any())
                {
                    var bookings = GetBookings();
                    _dbContext.Bookings.AddRange(bookings);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    Name = "Hotel Alpha",
                    Address = "123 Alpha Street",
                    Rooms = new List<Room>
                    {
                        new Room { RoomNumber = "101", Area = 20.5m, PricePerNight = 100.0m },
                        new Room { RoomNumber = "102", Area = 25.0m, PricePerNight = 120.0m }
                    }
                },
                new Hotel
                {
                    Name = "Hotel Beta",
                    Address = "456 Beta Avenue",
                    Rooms = new List<Room>
                    {
                        new Room { RoomNumber = "201", Area = 22.0m, PricePerNight = 110.0m },
                        new Room { RoomNumber = "202", Area = 30.0m, PricePerNight = 150.0m }
                    }
                }
            };
        }

        private IEnumerable<Room> GetRooms()
        {
            return new List<Room>
            {
                new Room { RoomNumber = "101", Area = 20.5m, PricePerNight = 100.0m },
                new Room { RoomNumber = "102", Area = 25.0m, PricePerNight = 120.0m },
                new Room { RoomNumber = "201", Area = 22.0m, PricePerNight = 110.0m },
                new Room { RoomNumber = "202", Area = 30.0m, PricePerNight = 150.0m }
            };
        }

     
        private IEnumerable<Booking> GetBookings()
        {
            return new List<Booking>
            {
                new Booking
                {
                    CheckInDate = DateTime.UtcNow.AddDays(5), 
                    CheckOutDate = DateTime.UtcNow.AddDays(7), 
                    RoomId = 1 
                },
                new Booking
                {
                    CheckInDate = DateTime.UtcNow.AddDays(10),
                    CheckOutDate = DateTime.UtcNow.AddDays(12),
                    RoomId = 2 
                },
                new Booking
                {
                    CheckInDate = DateTime.UtcNow.AddDays(15),
                    CheckOutDate = DateTime.UtcNow.AddDays(17),
                    RoomId = 3 
                },
                new Booking
                {
                    CheckInDate = DateTime.UtcNow.AddDays(20),
                    CheckOutDate = DateTime.UtcNow.AddDays(22),
                    RoomId = 4 
                }
            };
        }
    }
}
