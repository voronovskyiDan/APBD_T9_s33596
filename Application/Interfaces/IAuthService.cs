using Application.DTOs.Auth;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto?> RegisterAsync(RegisterDto model);
        Task<UserDto?> LoginAsync(LoginDto model);
    }
}
