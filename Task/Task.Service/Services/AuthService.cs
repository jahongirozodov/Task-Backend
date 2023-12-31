using AutoMapper;
using System.Text;
using Task.Domain.Entites;
using Task.Service.Helpers;
using System.Security.Claims;
using Task.Service.Exceptions;
using Task.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Task.Service.DTOs.Users;

namespace Task.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthService(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.configuration = configuration;
        }

        public async Task<string> AuthentificateAsync(UserLoginDto dto)
        {
            var user = await userService.RetrieveByEmailAsync(dto.Email);
            var isCorrectPassword = PasswordHash.Verify(dto.Password, user.PasswordHash, user.Salt);
            if (user == null || !isCorrectPassword)
                throw new CustomException(400, "Email or password is incorrect");

            var mappedUser = this.mapper.Map<User>(user);
            return GenerateToken(mappedUser);
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.FirstName)
                }),
                Audience = configuration["JWT:Audience"],
                Issuer = configuration["JWT:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["JWT:Expire"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
