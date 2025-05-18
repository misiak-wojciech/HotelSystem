using HotelBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBookingSystem.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> entity)
        {
           
            entity.HasKey(r => r.Id);

            // Props
            entity.Property(r => r.RoomNumber)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.Property(r => r.Area)
                  .IsRequired()
                  .HasPrecision(10, 2);

            entity.Property(r => r.PricePerNight)
                  .IsRequired()
                  .HasPrecision(10, 2);


            // Relation
            entity.HasOne(r => r.Hotel)
                  .WithMany(h => h.Rooms)
                  .HasForeignKey(r => r.HotelId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(r => r.Bookings)
                  .WithOne(b => b.Room)
                  .HasForeignKey(b => b.RoomId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}