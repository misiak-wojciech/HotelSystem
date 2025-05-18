using AutoMapper;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Application.DTOs;


namespace HotelBookingSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Hotel, HotelDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<Booking, BookingDto>();

            
            CreateMap<HotelDto, Hotel>();
            CreateMap<RoomDto, Room>();
            CreateMap<BookingDto, Booking>();
        }

    }
}
