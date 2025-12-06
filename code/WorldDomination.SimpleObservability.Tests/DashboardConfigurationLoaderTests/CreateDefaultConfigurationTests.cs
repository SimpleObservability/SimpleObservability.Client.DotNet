namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

public class CreateDefaultConfigurationTests
{
    [Fact]
    public void CreateDefaultConfiguration_ShouldReturnValidDefaultConfiguration()
    {
        // Arrange.
        // Act.
        var config = DashboardConfigurationLoader.CreateDefaultConfiguration();

        // Assert.
        config.ShouldNotBeNull();
        config.Services.ShouldNotBeNull();
        config.Services.ShouldBeEmpty();
        config.RefreshIntervalSeconds.ShouldBe(30);
        config.TimeoutSeconds.ShouldBe(5);
        config.EnvironmentOrder.ShouldBeNull();
    }
}
