using Shouldly;

namespace TestContainers.BasicSamples.Tests.UnitTests;
public class MathServiceTests
{
    [Theory]
    [InlineData(7, 5, 12)]
    [InlineData(0, 0, 0)]
    [InlineData(-2, -3, -5)]
    [InlineData(int.MaxValue, 1, int.MinValue)] // overflow
    public void Add_ShouldReturnExpectedResult(int a, int b, int expected)
    {
        // Arrange
        var service = new MathServiceBuilder().Build();

        // Act
        var result = service.Add(a, b);

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData(10, 3, 7)]
    [InlineData(0, 0, 0)]
    [InlineData(-2, -3, 1)]
    [InlineData(int.MinValue, 1, int.MaxValue)] // underflow
    public void Subtract_ShouldReturnExpectedResult(int a, int b, int expected)
    {
        // Arrange
        var service = new MathServiceBuilder().Build();

        // Act
        var result = service.Subtract(a, b);

        // Assert
        result.ShouldBe(expected);
    }
}

internal class MathServiceBuilder
{
    public MathService Build()
    {
        return new MathService();
    }
}
