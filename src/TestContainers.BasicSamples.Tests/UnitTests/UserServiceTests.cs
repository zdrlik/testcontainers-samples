using NSubstitute;
using Shouldly;

namespace TestContainers.BasicSamples.Tests.UnitTests;

public class UserServiceTests
{
    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new UserEntity(1, "Alice", "alice@example.com");
        var repo = Substitute.For<IUserRepository>();
        repo.GetUserByIdAsync(1).Returns(user);
        var service = new UserService(repo);

        // Act
        var result = await service.GetUserByIdAsync(1);

        // Assert
        result.ShouldBe(user);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var repo = Substitute.For<IUserRepository>();
        repo.GetUserByIdAsync(Arg.Any<int>()).Returns((UserEntity?)null);
        var service = new UserService(repo);

        // Act
        var result = await service.GetUserByIdAsync(42);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<UserEntity>
        {
            new(1, "Alice", "alice@example.com"),
            new(2, "Bob", "bob@example.com")
        };
        var repo = Substitute.For<IUserRepository>();
        repo.GetAllUsersAsync().Returns(users);
        var service = new UserService(repo);

        // Act
        var result = await service.GetAllUsersAsync();

        // Assert
        result.ShouldBe(users);
    }
}