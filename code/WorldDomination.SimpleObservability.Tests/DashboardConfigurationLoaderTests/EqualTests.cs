namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

public class EqualTests
{
    [Fact]
    public void Equal_GivenEqualValues_ShouldBeEqual()
    {
        // Arrange.
        var services = new List<ServiceEndpoint>
        {
            new() { Name = "Service 1", Environment = "DEV", HealthCheckUrl = "http://localhost:5001" }
        };

        var config1 = new DashboardConfiguration
        {
            Services = services,
            RefreshIntervalSeconds = 60,
            TimeoutSeconds = 10
        };

        var config2 = new DashboardConfiguration
        {
            Services = services,
            RefreshIntervalSeconds = 60,
            TimeoutSeconds = 10
        };

        // Act.
        var areEqual = config1 == config2;

        // Assert.
        areEqual.ShouldBeTrue();
    }
}
