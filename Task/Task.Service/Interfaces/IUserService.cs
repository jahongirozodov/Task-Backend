using Task.Service.DTOs.Users;

namespace Task.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserResultDto> RegisterAsync(UserRegisterDto dto);
        Task<UserResultDto> RetrieveByEmailAsync(string email);
    }
}
