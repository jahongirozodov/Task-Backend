using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Api.Helpers;
using Task.Service.DTOs.Users;
using Task.Service.Interfaces;

namespace Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Authorize")]        
        
        public async Task<IActionResult> AuthenticateAsync([FromBody]UserLoginDto dto)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await authService.AuthentificateAsync(dto)
            });
        } 
    }
}
