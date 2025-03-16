using AuthAPI.Application.Common;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Enums;
using AuthAPI.Domain.Interfaces;
using AuthAPI.Domain.Settings;
using Microsoft.Extensions.Options;

namespace AuthAPI.Application.Services
{
    /// <summary>
    /// Service implementation for authentication operations
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly AuthSettings _authSettings;

        /// <summary>
        /// Constructor for AuthService
        /// </summary>
        /// <param name="unitOfWork">Unit of work for persistence operations</param>
        /// <param name="jwtService">JWT service for token management</param>
        /// <param name="authSettings">Authentication settings</param>
        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtService jwtService,
            IOptions<AuthSettings> authSettings)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _authSettings = authSettings.Value;
        }

        /// <inheritdoc/>
        public async Task<(bool Succeeded, string Message, object? Token)> LoginAsync(string email, string password, bool rememberMe)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByEmailAsync(email);
                if (user == null)
                {
                    return (false, "Invalid email or password", null);
                }

                // Check if user is blocked
                if (user.IsBlocked && user.BlockedUntil.HasValue && user.BlockedUntil > DateTime.UtcNow)
                {
                    var timeRemaining = user.BlockedUntil.Value - DateTime.UtcNow;
                    return (false, $"Account is temporarily blocked. Try again in {timeRemaining.Minutes} minutes.", null);
                }

                // Reset block if expired
                if (user.IsBlocked && user.BlockedUntil.HasValue && user.BlockedUntil <= DateTime.UtcNow)
                {
                    user.IsBlocked = false;
                    user.LoginFailedCount = 0;
                    user.BlockedUntil = null;
                }

                var isPasswordValid = await _unitOfWork.Users.CheckPasswordAsync(user, password);
                if (!isPasswordValid)
                {
                    // Increment failed login attempt count
                    user.LoginFailedCount++;

                    // Block account if max attempts reached
                    if (user.LoginFailedCount >= _authSettings.MaxLoginAttempts)
                    {
                        user.IsBlocked = true;
                        user.BlockedUntil = DateTime.UtcNow.AddMinutes(_authSettings.BlockDurationInMinutes);
                        await _unitOfWork.SaveChangesAsync();
                        return (false, $"Account is temporarily blocked due to multiple failed login attempts. Try again in {_authSettings.BlockDurationInMinutes} minutes.", null);
                    }

                    await _unitOfWork.SaveChangesAsync();
                    return (false, "Invalid email or password", null);
                }

                // Reset failed login attempts on successful login
                user.LoginFailedCount = 0;
                user.IsBlocked = false;
                user.BlockedUntil = null;
                user.LastLoginDate = DateTime.UtcNow;

                await _unitOfWork.SaveChangesAsync();

                var token = await _jwtService.GenerateTokenAsync(user, rememberMe);
                return (true, "Login successful", token);
            }
            catch (Exception ex)
            {
                return (false, $"Login failed: {ex.Message}", null);
            }
        }

        /// <inheritdoc/>
        public async Task<(bool Succeeded, string Message)> RegisterAsync(string firstName, string lastName, string email, string username, string password)
        {
            try
            {
                var existingEmail = await _unitOfWork.Users.GetByEmailAsync(email);
                if (existingEmail != null)
                {
                    return (false, "Email is already registered");
                }

                var existingUsername = await _unitOfWork.Users.GetByUsernameAsync(username);
                if (existingUsername != null)
                {
                    return (false, "Username is already taken");
                }

                var user = new ApplicationUser
                {
                    Email = email,
                    UserName = username,
                    FirstName = firstName,
                    LastName = lastName
                };

                var result = await _unitOfWork.Users.CreateUserAsync(user, password);
                if (!result)
                {
                    return (false, "Failed to create user");
                }

                // Add to default role
                await _unitOfWork.Users.AddToRoleAsync(user, UserRoles.User);
                await _unitOfWork.SaveChangesAsync();

                return (true, "Registration successful");
            }
            catch (Exception ex)
            {
                return (false, $"Registration failed: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public async Task<(bool Succeeded, string Message, object? Token)> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            try
            {
                var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
                var username = principal.Identity.Name;

                var user = await _unitOfWork.Users.GetByUsernameAsync(username);
                if (user == null ||
                    user.RefreshToken != refreshToken ||
                    user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    return (false, "Invalid token", null);
                }

                var newToken = await _jwtService.GenerateTokenAsync(user);
                return (true, "Token refreshed successfully", newToken);
            }
            catch (Exception ex)
            {
                return (false, $"Token refresh failed: {ex.Message}", null);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> RevokeTokenAsync(string username)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByUsernameAsync(username);
                if (user == null)
                {
                    return false;
                }

                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}