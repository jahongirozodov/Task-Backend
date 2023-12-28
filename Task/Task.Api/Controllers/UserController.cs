using Microsoft.AspNetCore.Mvc;
using Task.Api.Helpers;
using Task.Service.DTOs.Users;
using Task.Service.Interfaces;

namespace Task.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.userService.RegisterAsync(dto)
            });
        } 
    }
}