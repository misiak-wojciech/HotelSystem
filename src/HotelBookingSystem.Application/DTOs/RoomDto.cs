namespace HotelBookingSystem.Application.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public decimal Area { get; set; }
        public decimal PricePerNight { get; set; }
        public int HotelId { get; set; } 
    }
}
