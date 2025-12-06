namespace WorldDomination.SimpleObservability.Tests.HealthStatusTests;

public class EnumTests
{
    [Fact]
    public void HealthStatus_ShouldHaveThreeValues()
    {
        // Arrange.
        // Act.
        var values = Enum.GetValues<HealthStatus>();

        // Assert.
        values.Length.ShouldBe(3);
    }

    [Theory]
    [InlineData(HealthStatus.Healthy, 0)]
    [InlineData(HealthStatus.Degraded, 1)]
    [InlineData(HealthStatus.Unhealthy, 2)]
    public void HealthStatus_ShouldHaveCorrectIntegerValues(HealthStatus status, int expectedValue)
    {
        // Arrange.
        // Act.
        var actualValue = (int)status;

        // Assert.
        actualValue.ShouldBe(expectedValue);
    }

    [Theory]
    [InlineData(0, HealthStatus.Healthy)]
    [InlineData(1, HealthStatus.Degraded)]
    [InlineData(2, HealthStatus.Unhealthy)]
    public void HealthStatus_ShouldCastFromInteger(int value, HealthStatus expected)
    {
        // Arrange.
        // Act.
        var status = (HealthStatus)value;

        // Assert.
        status.ShouldBe(expected);
    }

    [Theory]
    [InlineData(HealthStatus.Healthy, "Healthy")]
    [InlineData(HealthStatus.Degraded, "Degraded")]
    [InlineData(HealthStatus.Unhealthy, "Unhealthy")]
    public void HealthStatus_ToString_ShouldReturnName(HealthStatus status, string expectedName)
    {
        // Arrange.
        // Act.
        var name = status.ToString();

        // Assert.
        name.ShouldBe(expectedName);
    }

}
