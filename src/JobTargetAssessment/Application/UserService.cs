using JobTargetAssessment.Domain;

namespace JobTargetAssessment.Application;

public interface IUserService
{
    public Task<List<User>> GetAllAsync();
    public Task<User?> GetByIdAsync(int id);
    public Task<bool> DeleteUserAsync(int id);
    public Task<User?> UpdateAsync(User user);
    public Task<bool> CreateAsync(User user);
}


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            throw new Exception("user not found");
        }

        return await _userRepository.DeleteAsync(id);
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(user.Id);

        if (userToUpdate is null)
        {
            throw new Exception("user not found");
        }

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> CreateAsync(User user)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(user.Id);

        if (userToUpdate is not null)
        {
            throw new Exception("user exist");
        }

        return await _userRepository.CreateAsync(user);
    }
}