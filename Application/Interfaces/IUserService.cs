using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserInfoDto>> GetAll();
    }
}
