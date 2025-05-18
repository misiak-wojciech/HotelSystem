using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Application.Interfaces
{
    public interface IHotelRepository
    {
        Task<Hotel> GetByIdAsync(int id);
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task AddAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
    }
}
