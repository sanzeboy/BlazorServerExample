using PopulationPortal.Application.Services.Populations.Models.Dtos;
using PopulationPortal.Application.Services.Populations.Models.Filters;

namespace PopulationPortal.Application.Services.Populations
{
    public interface IPopulationService
    {
        Task<AgeGroupPopulationCountDto> GetAgeGroupPopulationAsync(PopulationFilter filter);
        Task<bool> InsertUpdatePopulationAsync(InsertUpdatePopulationCountDto populationDto);
        Task<GenderPopulationCountDto> GetGenderPopulationAsync(PopulationFilter filter);
    }
}