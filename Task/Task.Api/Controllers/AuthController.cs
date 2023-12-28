using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Api.Helpers;
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
        [HttpGet("Authorize")]        
        
        public async Task<IActionResult> AuthenticateAsync(string email,string password)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await authService.AuthentificateAsync(email, password)
            });
        } 
    }
}
