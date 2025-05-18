using HotelBookingSystem.Application.Interfaces;
using HotelBookingSystem.Domain.Entities;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSystem.Application.DTOs;

namespace HotelBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomController(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms(string sortBy = "price", string sortOrder = "asc")
        {
            var rooms = await _roomRepository.GetAllAsync(); 

        
            if (sortBy.ToLower() == "price")
            {
                if (sortOrder.ToLower() == "desc")
                {
                    rooms = rooms.OrderByDescending(r => r.PricePerNight).ToList(); 
                }
                else
                {
                    rooms = rooms.OrderBy(r => r.PricePerNight).ToList();  
                }
            }

            var roomDtos = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            return Ok(roomDtos);
        }



        // GET: api/room/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound($"Romm with ID {id} not found.");
            }
            var roomDto = _mapper.Map<RoomDto>(room);
            return Ok(roomDto);
        }

        // POST: api/room
        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] RoomDto roomDto)
        {
            if (roomDto == null)
            {
                return BadRequest("Room data is required.");
            }

            var room = _mapper.Map<Room>(roomDto);
            await _roomRepository.AddAsync(room);
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, roomDto);
        }

        // PUT: api/room/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomDto roomDto)
        {

            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }

            _mapper.Map(roomDto, room);
            await _roomRepository.UpdateAsync(room);
            return NoContent();
        }

        // DELETE: api/room/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }

            await _roomRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
