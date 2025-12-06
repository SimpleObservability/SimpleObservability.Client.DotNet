using Bogus;

namespace WorldDomination.SimpleObservability.Tests.HealthMetadataTests;

/// <summary>
/// Tests for the <see cref="HealthMetadata.Status"/> property.
/// </summary>
public class StatusTests
{
    private readonly Faker _faker = new();

    [Theory]
    [InlineData(HealthStatus.Healthy)]
    [InlineData(HealthStatus.Degraded)]
    [InlineData(HealthStatus.Unhealthy)]
    public void Status_WithDifferentStatuses_ShouldSetCorrectly(HealthStatus status)
    {
        // Arrange.
        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = _faker.System.Version().ToString(),
            Status = status
        };

        // Assert.
        metadata.Status.ShouldBe(status);
    }

    [Fact]
    public void Status_WithDefaultValue_ShouldBeHealthy()
    {
        // Arrange.
        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = _faker.System.Version().ToString()
        };

        // Assert.
        metadata.Status.ShouldBe(HealthStatus.Healthy);
    }
}
