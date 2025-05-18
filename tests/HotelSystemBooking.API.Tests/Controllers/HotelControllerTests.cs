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


namespace HotelBookingSystem.API.Controllers.Tests
{
    public class HotelControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IHotelRepository> _hotelRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        public HotelControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    
                    services.Replace(ServiceDescriptor.Scoped(typeof(IHotelRepository),
                                                _ => _hotelRepositoryMock.Object));
                    services.Replace(ServiceDescriptor.Scoped(typeof(IMapper),
                                                _ => _mapperMock.Object));
                });
            });
        }

        [Fact]
        public async Task GetHotel_ForNonExistingId_ShouldReturn404NotFound()
        {
            // Arrange
            var id = 999;
            _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Hotel?)null);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/Hotel/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetHotel_ForExistingId_ShouldReturn200Ok()
        {
            // Arrange
            var id = 1;
            var hotel = new Hotel { Id = id, Name = "Test Hotel", Address = "Test Address" };
            var hotelDto = new HotelDto { Id = id, Name = "Test Hotel", Address = "Test Address" };

            _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(hotel);
            _mapperMock.Setup(mapper => mapper.Map<HotelDto>(hotel)).Returns(hotelDto);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/Hotel/{id}");
            var result = await response.Content.ReadFromJsonAsync<HotelDto>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().NotBeNull();
            result!.Name.Should().Be("Test Hotel");
        }

        [Fact]
        public async Task GetHotels_ShouldReturnListOfHotels()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new Hotel { Id = 1, Name = "Hotel A", Address = "Address A" },
                new Hotel { Id = 2, Name = "Hotel B", Address = "Address B" }
            };
            var hotelDtos = hotels.Select(h => new HotelDto { Id = h.Id, Name = h.Name, Address = h.Address }).ToList();

            _hotelRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(hotels);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<HotelDto>>(hotels)).Returns(hotelDtos);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hotel");
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<HotelDto>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task DeleteHotel_ForExistingId_ShouldReturn204NoContent()
        {
            // Arrange
            var id = 1;
            var hotel = new Hotel { Id = id, Name = "Hotel X" };

            _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(hotel);
            _hotelRepositoryMock.Setup(repo => repo.DeleteAsync(id)).Returns(Task.CompletedTask);

            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"/api/Hotel/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteHotel_ForNonExistingId_ShouldReturn404NotFound()
        {
            // Arrange
            var id = 100;
            _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Hotel?)null);

            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"/api/Hotel/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
