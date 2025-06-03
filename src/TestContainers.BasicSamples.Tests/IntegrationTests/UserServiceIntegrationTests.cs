using Shouldly;

namespace TestContainers.BasicSamples.Tests.IntegrationTests;
public class UserServiceIntegrationTests : IClassFixture<IntegrationTestsFixture>
{
    private readonly IntegrationTestsFixture fixture;

    public UserServiceIntegrationTests(IntegrationTestsFixture fixture)
    {
        this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange        
        var service = new UserService(new SqlUserRepository(fixture.ConnectionString));

        // Act
        var result = await service.GetUserByIdAsync(1);

        // Assert
        result.ShouldNotBeNull();
        result!.Id.ShouldBe(1);
        result.Name.ShouldBe("Alice");
        result.Email.ShouldBe("alice@example.com");
    }
}
