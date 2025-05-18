using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Application.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetAllAsync();
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
    }
}
