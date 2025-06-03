namespace TestContainers.BasicSamples;

public record UserEntity(int Id, string Name, string Email);

public interface IUserRepository
{
    Task<UserEntity?> GetUserByIdAsync(int id);
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
}
