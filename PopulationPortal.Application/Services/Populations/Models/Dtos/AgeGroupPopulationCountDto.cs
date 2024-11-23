using PopulationPortal.Domain.Entities.Populations;

namespace PopulationPortal.Application.Services.Populations.Models.Dtos
{
    public class AgeGroupPopulationCountDto
    {
        public long Old { get; set; }
        public long Young { get; set; }
        public long Child { get; set; }
    }
}
