using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace PopulationPortal.Application.Infrastructures
{
    public class ICurrentUserService
    {
        public bool IsAuthenticated { get; }
        public HttpContext HttpContext { get; }
        public int AppUserId { get; }
    }
}
