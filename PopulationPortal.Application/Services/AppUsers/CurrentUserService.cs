using Microsoft.AspNetCore.Http;
using PopulationPortal.Application.Exceptions;
using PopulationPortal.Application.Infrastructures;
using System.Security.Claims;

namespace PopulationPortal.Application.Services.AppUsers
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor
          )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity.IsAuthenticated ?? false;

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public int AppUserId
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity)?.FindFirst(ClaimTypes.NameIdentifier);
                if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                    throw new UnAuthorizedException("User is not authenticated");

                try
                {
                    return int.Parse(claim.Value);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


    }
}
