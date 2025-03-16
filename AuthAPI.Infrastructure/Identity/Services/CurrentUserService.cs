using AuthAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AuthAPI.Infrastructure.Identity.Services
{
    /// <summary>
    /// Service for accessing the current user's information from the HTTP context
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor for CurrentUserService
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor for accessing the current request</param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

        public IEnumerable<Claim> Claims => _httpContextAccessor.HttpContext?.User?.Claims ?? Array.Empty<Claim>();

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
        }
    }
}