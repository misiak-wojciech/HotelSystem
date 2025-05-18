namespace HotelBookingSystem.Application.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set; } 
    }
}
