using Application.DTOs.Auth;
using Application.Interfaces;
using BCrypt.Net;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserDto?> LoginAsync(LoginDto model)
        {
            var email = model.Email.ToLowerInvariant();

            var user = await _userRepository.GetByEmailAsync(email);

            if (user is null)
                return null;

            if (!_passwordHasher.Verify(model.Password, user.PasswordHash))
                return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<UserDto?> RegisterAsync(RegisterDto model)
        {
            var email = model.Email.ToLowerInvariant();

            if (await _userRepository.ExistsByEmaiAsync(email))
                return null;
           

            var user = new User
            {
                Email = email,
                PasswordHash = _passwordHasher.Hash(model.Password),
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return new UserDto {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
