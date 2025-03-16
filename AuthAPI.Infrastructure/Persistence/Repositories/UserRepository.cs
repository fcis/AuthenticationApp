using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthAPI.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Repository implementation for user management operations
    /// </summary>
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Constructor for UserRepository
        /// </summary>
        /// <param name="dbContext">Database context</param>
        /// <param name="userManager">Identity user manager</param>
        /// <param name="roleManager">Identity role manager</param>
        public UserRepository(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) : base(dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <inheritdoc/>
        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        /// <inheritdoc/>
        public async Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        /// <inheritdoc/>
        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        /// <inheritdoc/>
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        /// <inheritdoc/>
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        /// <inheritdoc/>
        public async Task<bool> AddToRoleAsync(ApplicationUser user, string role)
        {
            // Create role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, role);
            return result.Succeeded;
        }

        /// <inheritdoc/>
        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        /// <inheritdoc/>
        public async Task<bool> AddClaimAsync(ApplicationUser user, Claim claim)
        {
            var result = await _userManager.AddClaimAsync(user, claim);
            return result.Succeeded;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveClaimAsync(ApplicationUser user, Claim claim)
        {
            var result = await _userManager.RemoveClaimAsync(user, claim);
            return result.Succeeded;
        }
    }
}