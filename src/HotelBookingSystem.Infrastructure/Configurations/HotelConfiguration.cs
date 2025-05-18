using HotelBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBookingSystem.Infrastructure.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> entity)
        {
            
            entity.HasKey(h => h.Id);

            // Props
            entity.Property(h => h.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(h => h.Address)
                  .IsRequired()
                  .HasMaxLength(200);

            
            entity.HasMany(h => h.Rooms)
                  .WithOne(r => r.Hotel)
                  .HasForeignKey(r => r.HotelId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
