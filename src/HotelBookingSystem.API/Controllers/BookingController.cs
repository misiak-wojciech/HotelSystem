using HotelBookingSystem.Application.Interfaces;
using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        // GET: api/booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(bookingDtos);
        }

        // GET: api/booking/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }
            var bookingDto = _mapper.Map<BookingDto>(booking);
            return Ok(bookingDto);
        }

        // POST: api/booking
        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest("Booking data is required.");
            }

            var booking = _mapper.Map<Booking>(bookingDto);
            await _bookingRepository.AddAsync(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, bookingDto);
        }

        // PUT: api/booking/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDto bookingDto)
        {

            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }

        
            _mapper.Map(bookingDto, booking);
            await _bookingRepository.UpdateAsync(booking);
            return NoContent();
        }

        // DELETE: api/booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }

         
            await _bookingRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
