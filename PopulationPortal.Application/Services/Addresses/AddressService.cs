using Microsoft.EntityFrameworkCore;
using PopulationPortal.Application.Infrastructures;
using PopulationPortal.Application.Services.Addresses.Models;
using PopulationPortal.Domain.Entities.Addresses;

namespace PopulationPortal.Application.Services.Addresses
{
    public class AddressService : IAddressService
    {
        private readonly IApplicationDbContextFactory _contextFactory;

        public AddressService(IApplicationDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // Country Methods
        public async Task<List<CountryDto>> GetAllCountriesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Countries
                .Select(c => new CountryDto { Id = c.Id, Name = c.Name })
                .ToListAsync();
        }

        public async Task<CountryDto?> GetCountryByIdAsync(int countryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var country = await context.Countries.FindAsync(countryId);
            return country == null ? null : new CountryDto { Id = country.Id, Name = country.Name };
        }

        public async Task<int> CreateCountryAsync(CountryDto countryDto)
        {
            using var context = _contextFactory.CreateDbContext();
            var country = new Country { Name = countryDto.Name };
            context.Countries.Add(country);
            await context.SaveChangesAsync();
            return country.Id;
        }

        public async Task UpdateCountryAsync(CountryDto countryDto)
        {
            using var context = _contextFactory.CreateDbContext();
            var country = await context.Countries.FindAsync(countryDto.Id);
            if (country == null)
                throw new KeyNotFoundException("Country not found");

            country.Name = countryDto.Name;
            await context.SaveChangesAsync();
        }

        public async Task DeleteCountryAsync(int countryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var country = await context.Countries.FindAsync(countryId);
            if (country == null)
                throw new KeyNotFoundException("Country not found");

            context.Countries.Remove(country);
            await context.SaveChangesAsync();
        }

        // City Methods
        public async Task<List<CityDto>> GetCitiesByCountryIdAsync(int countryId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Cities
                .Where(c => c.CountryId == countryId)
                .Select(c => new CityDto { Id = c.Id, CountryId = c.CountryId, Name = c.Name })
                .ToListAsync();
        }

        public async Task<CityDto?> GetCityByIdAsync(int cityId)
        {
            using var context = _contextFactory.CreateDbContext();
            var city = await context.Cities.FindAsync(cityId);
            return city == null ? null : new CityDto { Id = city.Id, CountryId = city.CountryId, Name = city.Name };
        }

        public async Task<int> CreateCityAsync(CityDto cityDto)
        {
            using var context = _contextFactory.CreateDbContext();
            var city = new City { CountryId = cityDto.CountryId, Name = cityDto.Name };
            context.Cities.Add(city);
            await context.SaveChangesAsync();
            return city.Id;
        }
            
        public async Task UpdateCityAsync(CityDto cityDto)
        {
            using var context = _contextFactory.CreateDbContext();
            var city = await context.Cities.FindAsync(cityDto.Id);
            if (city == null)
                throw new KeyNotFoundException("City not found");

            city.Name = cityDto.Name;
            city.CountryId = cityDto.CountryId;
            await context.SaveChangesAsync();
        }

        public async Task DeleteCityAsync(int cityId)
        {
            using var context = _contextFactory.CreateDbContext();
            var city = await context.Cities.FindAsync(cityId);
            if (city == null)
                throw new KeyNotFoundException("City not found");

            context.Cities.Remove(city);
            await context.SaveChangesAsync();
        }
    }
}
