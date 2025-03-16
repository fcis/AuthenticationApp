using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Interfaces
{
    public interface IUserService
    {
        Task<(bool Succeeded, string Message, object? User)> GetUserByIdAsync(string userId);
        Task<(bool Succeeded, string Message, object? User)> GetUserByEmailAsync(string email);
        Task<(bool Succeeded, string Message, object? User)> GetUserByUsernameAsync(string username);
        Task<(bool Succeeded, string Message, object? Users)> GetAllUsersAsync();
        Task<(bool Succeeded, string Message, object? User)> UpdateUserAsync(string userId, string firstName, string lastName, string email, string? currentPassword, string? newPassword);
        Task<(bool Succeeded, string Message)> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
