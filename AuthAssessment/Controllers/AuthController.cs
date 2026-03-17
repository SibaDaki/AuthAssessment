using Microsoft.AspNetCore.Mvc;
using AuthAssessment.Services.IServices;
using AuthAssessment.DTO.Response;
using AuthAssessment.DTO.Request;
using RegisterRequest = AuthAssessment.DTO.Request.RegisterRequest;
using LoginRequest = AuthAssessment.DTO.Request.LoginRequest;
using IAuthenticationService = AuthAssessment.Services.IServices.IAuthenticationService;


namespace AuthAssessment.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<UserDto> { Success = false, Message = "Invalid request" });

            var result = await _authenticationService.RegisterAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new LoginResponse { Success = false, Message = "Invalid request" });

            var result = await _authenticationService.LoginAsync(request);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
