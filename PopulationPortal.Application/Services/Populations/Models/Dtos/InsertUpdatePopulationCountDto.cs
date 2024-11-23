using PopulationPortal.Domain.Entities.Populations;
using System.ComponentModel.DataAnnotations;

namespace PopulationPortal.Application.Services.Populations.Models.Dtos
{
    public class InsertUpdatePopulationCountDto
    {
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public AgeGroupEnum AgeGroup { get; set; }
        [Required]
        public long Male { get; set; }
        [Required]
        public long Female { get; set; }
    }
}
