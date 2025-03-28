using APIDay1.DTO;
using APIDay1.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIDay1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var result = _authService.Login(loginDTO);

            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return Unauthorized(result.Message);
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto userDto)
        {
            var result = _authService.Register(userDto);
            if (result.IsSuccess)
            {
                return Ok(new
                {
                    message = "Registered correctly"
                });
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("add-role")]
        public IActionResult AddRole(RoleDto roleDto)
        {
            var result = _authService.AddRole(roleDto);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
