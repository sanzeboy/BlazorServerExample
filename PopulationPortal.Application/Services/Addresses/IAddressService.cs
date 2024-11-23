using PopulationPortal.Application.Services.Addresses.Models;

namespace PopulationPortal.Application.Services.Addresses
{
    public interface IAddressService
    {
        // Country Methods
        Task<List<CountryDto>> GetAllCountriesAsync();
        Task<CountryDto?> GetCountryByIdAsync(int countryId);
        Task<int> CreateCountryAsync(CountryDto countryDto);
        Task UpdateCountryAsync(CountryDto countryDto);
        Task DeleteCountryAsync(int countryId);

        // City Methods
        Task<List<CityDto>> GetCitiesByCountryIdAsync(int countryId);
        Task<CityDto?> GetCityByIdAsync(int cityId);
        Task<int> CreateCityAsync(CityDto cityDto);
        Task UpdateCityAsync(CityDto cityDto);
        Task DeleteCityAsync(int cityId);
    }
}