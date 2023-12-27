using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Entites;
using Task.Service.Exceptions;
using Task.Service.Helpers;
using Task.Service.Interfaces;

namespace Task.Service.Services
{
    internal class AuthService : IAuthService
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

        public async Task<string> AuthentificateAsync(string email, string password)
        {
            var user = await userService.RetrieveByEmailAsync(email);
            if (user == null || ! PasswordHash.Verify(password, user.Password,user.Salt))
                throw new CustomException(400, "Email or password is incorrect");

            var mappedUser = this.mapper.Map<User>(user);
            return GenerateToken(mappedUser);
        }

        public string GenerateToken(User user)
        {        var tokenHandler = new JwtSecurityTokenHandler();
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
