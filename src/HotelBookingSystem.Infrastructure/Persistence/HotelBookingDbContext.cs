using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Configurations;
using HotelBookingSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;


namespace HotelBookingSystem.Infrastructure.Persistence
{
    public class HotelBookingDbContext : DbContext
    {
       
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

      
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HotelConfiguration());
            builder.ApplyConfiguration(new RoomConfiguration());
            builder.ApplyConfiguration(new BookingConfiguration());

            base.OnModelCreating(builder);
        }

    }
}