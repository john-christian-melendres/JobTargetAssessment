using JobTargetAssessment.Domain;

namespace JobTargetAssessment.Persistence;

public class UserRepository : IUserRepository
{
    private readonly JsonDbContext _dbContext;
    public UserRepository(JsonDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return (await _dbContext.ReadDataAsync<User>()).ToList();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var result = (await _dbContext.ReadDataAsync<User>()).ToList();
        return result.FirstOrDefault(r => r.Id == id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = (await _dbContext.ReadDataAsync<User>()).ToList();
        var deleteUserCount = result.RemoveAll(r => r.Id == id);

        if (deleteUserCount == 1)
        {
            await _dbContext.WriteDataAsync(result);
        }

        return deleteUserCount == 1;
    }

    public async Task<User?> UpdateAsync(User user)
    {
    var result = (await _dbContext.ReadDataAsync<User>()).ToList();
    var index = result.FindIndex(r => r.Id == user.Id);

    if (index >= 0)
    {

        result[index] = user;
        await _dbContext.WriteDataAsync(result);
        return user;
    }

    return null;
    }

    public async Task<bool> CreateAsync(User user)
    {
        var result = (await _dbContext.ReadDataAsync<User>()).ToList();
        var currentCount = result.Count();

        var lastUserIdCount = result.Last();

        if (lastUserIdCount is null)
        {
            user.Id = 1;
        }
        else
        {
            user.Id = currentCount == lastUserIdCount.Id ? lastUserIdCount.Id + 1 : currentCount + 1;
        }

        result.Add(user);
        await _dbContext.WriteDataAsync(result);

        var newResult = (await _dbContext.ReadDataAsync<User>()).ToList();
        var newResultCount = newResult.Count();

        return newResultCount > currentCount;
    }
}