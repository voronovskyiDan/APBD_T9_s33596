using Application.DTOs;
using Application.Interfaces;
using Domain.Repositories;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserInfoDto>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserInfoDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList();
        }
    }
}
