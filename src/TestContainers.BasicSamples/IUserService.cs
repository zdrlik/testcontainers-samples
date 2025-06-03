namespace TestContainers.BasicSamples;

public interface IUserService
{
    Task<UserEntity?> GetUserByIdAsync(int id);
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
}
