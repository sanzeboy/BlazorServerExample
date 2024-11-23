using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PopulationPortal.Application.HelperClasses;
using PopulationPortal.Application.Infrastructures;
using PopulationPortal.Application.Services.AppUsers.Models.Dtos;
using PopulationPortal.Domain.Entities.AppUsers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PopulationPortal.Application.Services.AppUsers
{
    public class AppUserService : IAppUserService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;

        public AppUserService(ICurrentUserService currentUserService,
            IConfiguration configuration,
            IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _configuration = configuration;
            _context = context;
        }

        public async Task<LoginResponse> Login(LoginDto request)
        {
            var user = await _context.AppUsers
                .FirstOrDefaultAsync(a => a.Username == request.Username);

            if (user == null)
                return new LoginResponse
                {
                    IsSuccess = false,
                    Message = "Invalid username or password"
                };
            if (!PasswordHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return new LoginResponse
                {
                    IsSuccess = false,
                    Message = "Invalid username or password"
                };

            return new LoginResponse
            {
                IsSuccess = true,
            };
        }

    }
}
