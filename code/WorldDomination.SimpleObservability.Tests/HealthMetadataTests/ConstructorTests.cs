using Bogus;

namespace WorldDomination.SimpleObservability.Tests.HealthMetadataTests;

/// <summary>
/// Tests for the <see cref="HealthMetadata"/> constructor.
/// </summary>
public class ConstructorTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Constructor_WithRequiredProperties_ShouldCreateInstance()
    {
        // Arrange.
        var serviceName = _faker.Company.CompanyName();
        var version = _faker.System.Version().ToString();

        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = serviceName,
            Version = version
        };

        // Assert.
        metadata.ServiceName.ShouldBe(serviceName);
        metadata.Version.ShouldBe(version);
        metadata.Status.ShouldBe(HealthStatus.Healthy);
        metadata.Timestamp.ShouldBeInRange(DateTimeOffset.UtcNow.AddSeconds(-5), DateTimeOffset.UtcNow.AddSeconds(1));
        metadata.Environment.ShouldBeNull();
        metadata.AdditionalMetadata.ShouldBeNull();
        metadata.Description.ShouldBeNull();
        metadata.HostName.ShouldBeNull();
        metadata.Uptime.ShouldBeNull();
    }

    [Fact]
    public void Constructor_WithAllProperties_ShouldCreateInstance()
    {
        // Arrange.
        var serviceName = _faker.Company.CompanyName();
        var version = _faker.System.Version().ToString();
        var environment = _faker.PickRandom("DEV", "UAT", "PROD");
        var status = _faker.PickRandom<HealthStatus>();
        var timestamp = DateTimeOffset.UtcNow;
        var additionalMetadata = new Dictionary<string, string>
        {
            ["Database"] = "Connected",
            ["Cache"] = "Redis v7.0"
        };
        var description = _faker.Lorem.Sentence();
        var hostName = _faker.Internet.DomainName();
        var uptime = TimeSpan.FromHours(_faker.Random.Double(1, 1000));

        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = serviceName,
            Version = version,
            Environment = environment,
            Status = status,
            Timestamp = timestamp,
            AdditionalMetadata = additionalMetadata,
            Description = description,
            HostName = hostName,
            Uptime = uptime
        };

        // Assert.
        metadata.ServiceName.ShouldBe(serviceName);
        metadata.Version.ShouldBe(version);
        metadata.Environment.ShouldBe(environment);
        metadata.Status.ShouldBe(status);
        metadata.Timestamp.ShouldBe(timestamp);
        metadata.AdditionalMetadata.ShouldBe(additionalMetadata);
        metadata.Description.ShouldBe(description);
        metadata.HostName.ShouldBe(hostName);
        metadata.Uptime.ShouldBe(uptime);
    }
}
