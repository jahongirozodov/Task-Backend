using AutoMapper;
using Task.Domain.Entites;
using Task.Service.Helpers;
using Task.Service.Exceptions;
using Task.Service.DTOs.Users;
using Task.Service.Interfaces;
using Task.Data.IRepositories;

namespace Task.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> repository;
        public UserService(IMapper mapper, IRepository<User> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<UserResultDto> RegisterAsync(UserRegisterDto dto)
        {
            var existUser = await this.repository.GetAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (existUser is not null)
                throw new AlreadyExistException("This user is already exist!");

            var hashSalt = PasswordHash.Hasher(dto.PasswordHash);

            var mappedUser = this.mapper.Map<User>(dto);
            
            mappedUser.PasswordHash = hashSalt.passwordHash;
            mappedUser.Salt = hashSalt.salt;

            await this.repository.CreateAsync(mappedUser);
            await this.repository.SaveAsync();

            return this.mapper.Map<UserResultDto>(mappedUser);
        }

        public async Task<UserResultDto> RetrieveByEmailAsync(string email)
        {
            var user = await this.repository.GetAsync(u => u.Email.ToLower().Equals(email.ToLower()))
                                 ?? throw new AlreadyExistException("This user is not found!");

            return mapper.Map<UserResultDto>(user);
        }
    }
}
