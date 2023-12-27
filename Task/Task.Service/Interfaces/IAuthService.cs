using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Entites;

namespace Task.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthentificateAsync(string email, string password);
        string GenerateToken(User user);
    }
}
