using System.ComponentModel.DataAnnotations;

namespace PopulationPortal.Application.Services.Addresses.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
