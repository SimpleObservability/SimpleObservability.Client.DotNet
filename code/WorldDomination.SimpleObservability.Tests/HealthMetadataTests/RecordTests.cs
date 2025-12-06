using Bogus;

namespace WorldDomination.SimpleObservability.Tests.HealthMetadataTests;

public class RecordTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Record_WithEqualValues_ShouldBeEqual()
    {
        // Arrange.
        var serviceName = _faker.Company.CompanyName();
        var version = _faker.System.Version().ToString();
        var timestamp = DateTimeOffset.UtcNow;

        var metadata1 = new HealthMetadata
        {
            ServiceName = serviceName,
            Version = version,
            Timestamp = timestamp
        };

        var metadata2 = new HealthMetadata
        {
            ServiceName = serviceName,
            Version = version,
            Timestamp = timestamp
        };

        // Act.
        var areEqual = metadata1 == metadata2;

        // Assert.
        areEqual.ShouldBeTrue();
        metadata1.GetHashCode().ShouldBe(metadata2.GetHashCode());
    }

    [Fact]
    public void Record_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange.
        var metadata1 = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = "1.0.0"
        };

        var metadata2 = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = "2.0.0"
        };

        // Act.
        var areEqual = metadata1 == metadata2;

        // Assert.
        areEqual.ShouldBeFalse();
    }
}
