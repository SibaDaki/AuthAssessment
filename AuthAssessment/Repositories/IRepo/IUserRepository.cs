using AuthAssessment.Entities;

namespace AuthAssessment.Repositories.IRepo
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task SaveChangesAsync();
    }
}
