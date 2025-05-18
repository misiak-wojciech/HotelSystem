namespace HotelBookingSystem.Domain.Entities
{

    public class Booking
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!; // Właściwość nawigacyjna
    }
}