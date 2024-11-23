using System.ComponentModel.DataAnnotations;

namespace PopulationPortal.Application.Services.Addresses.Models
{
    public class CountryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
