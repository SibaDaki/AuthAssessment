using AuthAssessment.DTO.Request;
using AuthAssessment.DTO.Response;


namespace AuthAssessment.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<UserDto>> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
