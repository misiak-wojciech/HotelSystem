using HotelBookingSystem.Application.Interfaces;
using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            var hotelDtos = _mapper.Map<IEnumerable<HotelDto>>(hotels);
            return Ok(hotelDtos);
        }

        // GET: api/hotel/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound($"Hotel with ID {id} not found.");
            }
            var hotelDto = _mapper.Map<HotelDto>(hotel);
            return Ok(hotelDto);
        }

        // POST: api/hotel
        [HttpPost]
        public async Task<ActionResult<HotelDto>> CreateHotel([FromBody] HotelDto hotelDto)
        {
            if (hotelDto == null)
            {
                return BadRequest("Hotel data is required.");
            }

            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _hotelRepository.AddAsync(hotel);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotelDto);
        }

        // PUT: api/hotel/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelDto hotelDto)
        {
  
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound($"Hotel with ID {id} not found.");
            }

            _mapper.Map(hotelDto, hotel);
            await _hotelRepository.UpdateAsync(hotel);
            return NoContent();
        }

        // DELETE: api/hotel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound($"Hotel with ID {id} not found.");
            }

            await _hotelRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
