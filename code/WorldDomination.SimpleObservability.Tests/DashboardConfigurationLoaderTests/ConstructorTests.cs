using Bogus;

namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

public class ConstructorTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Constructor_WithRequiredProperties_ShouldCreateInstance()
    {
        // Arrange.
        var services = new List<ServiceEndpoint>
        {
            new()
            {
                Name = _faker.Company.CompanyName(),
                Environment = "DEV",
                HealthCheckUrl = _faker.Internet.Url()
            }
        };

        // Act.
        var config = new DashboardConfiguration
        {
            Services = services
        };

        // Assert.
        config.Services.ShouldBe(services);
        config.RefreshIntervalSeconds.ShouldBe(30);
        config.TimeoutSeconds.ShouldBe(5);
        config.EnvironmentOrder.ShouldBeNull();
    }

    [Fact]
    public void Constructor_WithAllProperties_ShouldCreateInstance()
    {
        // Arrange.
        var services = new List<ServiceEndpoint>
        {
            new()
            {
                Name = _faker.Company.CompanyName(),
                Environment = "DEV",
                HealthCheckUrl = _faker.Internet.Url()
            }
        };
        var refreshInterval = _faker.Random.Int(10, 120);
        var timeout = _faker.Random.Int(1, 30);
        var environmentOrder = new List<string> { "PROD", "UAT", "DEV" };

        // Act.
        var config = new DashboardConfiguration
        {
            Services = services,
            RefreshIntervalSeconds = refreshInterval,
            TimeoutSeconds = timeout,
            EnvironmentOrder = environmentOrder
        };

        // Assert.
        config.Services.ShouldBe(services);
        config.RefreshIntervalSeconds.ShouldBe(refreshInterval);
        config.TimeoutSeconds.ShouldBe(timeout);
        config.EnvironmentOrder.ShouldBe(environmentOrder);
    }
}
