using System.ComponentModel.DataAnnotations;

namespace PopulationPortal.Application.Services.AppUsers.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
