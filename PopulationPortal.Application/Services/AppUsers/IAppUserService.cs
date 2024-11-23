using PopulationPortal.Application.Services.AppUsers.Models.Dtos;

namespace PopulationPortal.Application.Services.AppUsers
{
    public interface IAppUserService
    {
        Task<LoginResponse> Login(LoginDto request);

    }
}
