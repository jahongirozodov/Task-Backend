using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Entites;
using Task.Service.DTOs.Users;

namespace Task.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthentificateAsync(UserLoginDto dto);
        string GenerateToken(User user);
    }
}
