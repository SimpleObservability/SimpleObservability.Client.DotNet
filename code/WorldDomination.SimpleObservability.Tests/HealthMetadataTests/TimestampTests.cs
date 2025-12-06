using Bogus;

namespace WorldDomination.SimpleObservability.Tests.HealthMetadataTests;

/// <summary>
/// Tests for the <see cref="HealthMetadata.Timestamp"/> property.
/// </summary>
public class TimestampTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Timestamp_DefaultValue_ShouldBeApproximatelyNow()
    {
        // Arrange.
        var before = DateTimeOffset.UtcNow;

        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = _faker.System.Version().ToString()
        };

        // Assert.
        var after = DateTimeOffset.UtcNow;
        metadata.Timestamp.ShouldBeInRange(before, after);
    }

    [Fact]
    public void Timestamp_WithCustomValue_ShouldSetCorrectly()
    {
        // Arrange.
        var customTimestamp = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);

        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = _faker.System.Version().ToString(),
            Timestamp = customTimestamp
        };

        // Assert.
        metadata.Timestamp.ShouldBe(customTimestamp);
    }
}
