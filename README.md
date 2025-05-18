HotelBookingSystem (.NET 8 Web API – Clean Architecture)
========================================================

HotelBookingSystem is a modular Web API built with .NET 8.0, designed to manage hotels, rooms, and bookings.  
It follows Clean Architecture principles and uses modern .NET technologies such as EF Core, AutoMapper, xUnit, and more.

--------------------------------------------------------
Technologies Used
--------------------------------------------------------

- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core (with Fluent API & Seed Data)
- AutoMapper
- xUnit
- FluentAssertions
- Moq 
- SQL Server Express

--------------------------------------------------------
Project Structure
--------------------------------------------------------

- `HotelBookingSystem.API` – API layer (controllers, DI setup, entry point)
- `HotelBookingSystem.Application` – Application logic (services, DTOs, interfaces)
- `HotelBookingSystem.Domain` – Domain models, enums, and business rules
- `HotelBookingSystem.Infrastructure` – EF Core context, Fluent API configs, repositories
- `HotelBookingSystem.Tests` – Unit and integration tests 

--------------------------------------------------------
Prerequisites
--------------------------------------------------------

- .NET 8.0 SDK
- Visual Studio 2022 or newer
- SQL Server Express or local SQL Server instance


