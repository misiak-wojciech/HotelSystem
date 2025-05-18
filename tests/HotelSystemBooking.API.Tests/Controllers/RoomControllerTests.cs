using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http.Json;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Application.Interfaces;
using AutoMapper;

namespace HotelSystemBooking.API.Tests.Controllers
{
    public class RoomControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRoomRepository> _roomRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        public RoomControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.Replace(ServiceDescriptor.Scoped(typeof(IRoomRepository), _ => _roomRepositoryMock.Object));
                    services.Replace(ServiceDescriptor.Scoped(typeof(IMapper), _ => _mapperMock.Object));
                });
            });
        }

        [Fact]
        public async Task GetRoom_ForNonExistingId_ShouldReturn404NotFound()
        {
            var id = 999;
            _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Room?)null);

            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Room/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetRoom_ForExistingId_ShouldReturn200Ok()
        {
            var id = 1;
            var room = new Room { Id = id, RoomNumber = "101", PricePerNight = 150 };
            var roomDto = new RoomDto { Id = id, RoomNumber = "101", PricePerNight = 150 };

            _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(room);
            _mapperMock.Setup(mapper => mapper.Map<RoomDto>(room)).Returns(roomDto);

            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Room/{id}");
            var result = await response.Content.ReadFromJsonAsync<RoomDto>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().NotBeNull();
            result!.RoomNumber.Should().Be("101");
        }

        [Fact]
        public async Task GetRooms_ShouldReturnListOfRooms()
        {
            var rooms = new List<Room>
            {
                new Room { Id = 1, RoomNumber = "101", PricePerNight = 100 },
                new Room { Id = 2, RoomNumber = "102", PricePerNight = 200 }
            };

            var roomDtos = rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                PricePerNight = r.PricePerNight
            }).ToList();

            _roomRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(rooms);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<RoomDto>>(rooms)).Returns(roomDtos);

            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Room");
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<RoomDto>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task DeleteRoom_ForExistingId_ShouldReturn204NoContent()
        {
            var id = 1;
            var room = new Room { Id = id, RoomNumber = "103" };

            _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(room);
            _roomRepositoryMock.Setup(repo => repo.DeleteAsync(id)).Returns(Task.CompletedTask);

            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/Room/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteRoom_ForNonExistingId_ShouldReturn404NotFound()
        {
            var id = 404;
            _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Room?)null);

            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/Room/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
