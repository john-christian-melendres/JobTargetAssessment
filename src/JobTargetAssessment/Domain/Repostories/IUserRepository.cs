namespace JobTargetAssessment.Domain;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<User?> UpdateAsync(User user);
    Task<bool> CreateAsync(User user);
}
