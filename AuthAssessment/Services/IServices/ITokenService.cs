using AuthAssessment.Entities;

namespace AuthAssessment.Services.IServices
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
