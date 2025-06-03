namespace TestContainers.BasicSamples;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<UserEntity?> GetUserByIdAsync(int id)
    {
        return await userRepository.GetUserByIdAsync(id);
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        return await userRepository.GetAllUsersAsync();
    }
}
