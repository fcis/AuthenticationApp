using AuthAPI.Application.Common;
using AuthAPI.Application.DTOs;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Interfaces;
using AuthAPI.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthAPI.Application.Services
{
    /// <summary>
    /// Service implementation for JWT token operations
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthSettings _authSettings;

        /// <summary>
        /// Constructor for JwtService
        /// </summary>
        /// <param name="userManager">Identity user manager</param>
        /// <param name="authSettings">Authentication settings</param>
        public JwtService(
            UserManager<ApplicationUser> userManager,
            IOptions<AuthSettings> authSettings)
        {
            _userManager = userManager;
            _authSettings = authSettings.Value;
        }

        /// <inheritdoc/>
        public async Task<object> GenerateTokenAsync(ApplicationUser user, bool extendedExpiry = false)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            foreach (var userClaim in userClaims)
            {
                authClaims.Add(userClaim);
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key));

            // Determine token expiry based on rememberMe flag
            double expiryInMinutes = extendedExpiry
                ? _authSettings.ExtendedTokenExpiryInMinutes
                : _authSettings.TokenExpiryInMinutes;

            var token = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                expires: DateTime.Now.AddMinutes(expiryInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_authSettings.RefreshTokenExpiryInDays);
            await _userManager.UpdateAsync(user);

            var tokenDto = new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                UserName = user.UserName,
                Roles = userRoles.ToList(),
                Claims = userClaims.ToDictionary(c => c.Type, c => c.Value)
            };

            return tokenDto;
        }

        /// <inheritdoc/>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <inheritdoc/>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}