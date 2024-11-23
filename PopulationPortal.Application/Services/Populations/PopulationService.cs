using Microsoft.EntityFrameworkCore;
using PopulationPortal.Application.Infrastructures;
using PopulationPortal.Application.Services.Populations.Models.Dtos;
using PopulationPortal.Application.Services.Populations.Models.Filters;
using PopulationPortal.Domain.Entities.Populations;

namespace PopulationPortal.Application.Services.Populations
{
    public class PopulationService : IPopulationService
    {
        private readonly IApplicationDbContext _dbContext;

        public PopulationService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertUpdatePopulationAsync(InsertUpdatePopulationCountDto populationDto)
        {
            // Validate country and city
            var countryExists = await _dbContext.Countries.AnyAsync(c => c.Id == populationDto.CountryId);
            if (!countryExists)
                throw new Exception("Country does not exist.");

            var cityExists = await _dbContext.Cities.AnyAsync(c => c.Id == populationDto.CityId && c.CountryId == populationDto.CountryId);
            if (!cityExists)
                throw new Exception("City does not exist or does not belong to the specified country.");


            var population = await _dbContext.Populations
            .FirstOrDefaultAsync(p => p.CountryId == populationDto.CountryId
            && p.CityId == populationDto.CityId
            && p.AgeGroup == populationDto.AgeGroup);

            if (population == null)
            {
                population = new Population
                {
                    CityId = populationDto.CityId,
                    AgeGroup = populationDto.AgeGroup,
                    CountryId = populationDto.CountryId,
                };
                _dbContext.Populations.Add(population);
            }
            population.Male = populationDto.Male;
            population.Female = populationDto.Female;
            return (await _dbContext.SaveChangesAsync() > 0);
        }

        public async Task<AgeGroupPopulationCountDto> GetAgeGroupPopulationAsync(PopulationFilter filter)
        {
            var query = _dbContext.Populations.AsQueryable();

            // Filter by country if provided
            if (filter.CountryId.HasValue)
                query = query.Where(p => p.City.CountryId == filter.CountryId);

            // Filter by city if provided
            if (filter.CityId.HasValue)
                query = query.Where(p => p.CityId == filter.CityId);

            // Filter by gender if provided
            if (filter.Gender.HasValue)
            {
                query = filter.Gender switch
                {
                    GenderEnum.Male => query.Where(p => p.Male > 0),
                    GenderEnum.Female => query.Where(p => p.Female > 0),
                    _ => query
                };
            }

            // Aggregate data
            var result = await query
                .GroupBy(p => p.AgeGroup)
                .Select(g => new
                {
                    AgeGroup = g.Key,
                    TotalCount = filter.Gender == GenderEnum.Male
                        ? g.Sum(p => p.Male)
                        : filter.Gender == GenderEnum.Female
                            ? g.Sum(p => p.Female)
                            : g.Sum(p => p.Male + p.Female)
                })
                .ToListAsync();

            // Map result to PopulationCountDto
            var dto = new AgeGroupPopulationCountDto
            {
                Old = result.Where(r => r.AgeGroup == AgeGroupEnum.Old).Sum(r => r.TotalCount),
                Young = result.Where(r => r.AgeGroup == AgeGroupEnum.Young).Sum(r => r.TotalCount),
                Child = result.Where(r => r.AgeGroup == AgeGroupEnum.Child).Sum(r => r.TotalCount)
            };

            return dto;
        }


        public async Task<GenderPopulationCountDto> GetGenderPopulationAsync(PopulationFilter filter)
        {
            var query = _dbContext.Populations.AsQueryable();

            // Apply filters if provided
            if (filter.CountryId.HasValue)
                query = query.Where(p => p.City.CountryId == filter.CountryId);

            if (filter.CityId.HasValue)
                query = query.Where(p => p.CityId == filter.CityId);

            if (filter.AgeGroup.HasValue)
                query = query.Where(p => p.AgeGroup == filter.AgeGroup);

            // Aggregate the population counts
            var result = await query
                .GroupBy(p => 1) // Group by a constant value since we only want a single aggregate result
                .Select(g => new GenderPopulationCountDto
                {
                    Male = g.Sum(p => p.Male),
                    Female = g.Sum(p => p.Female)
                })
                .FirstOrDefaultAsync(); // Fetch the aggregated result

            // Return the result or a default object if no data exists
            return result ?? new GenderPopulationCountDto { Male = 0, Female = 0 };
        }
    }
}
