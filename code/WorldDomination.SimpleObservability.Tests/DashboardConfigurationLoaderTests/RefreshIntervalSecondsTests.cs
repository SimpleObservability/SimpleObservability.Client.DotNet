namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

public class RefreshIntervalSecondsTests
{
    [Fact]
    public void RefreshIntervalSeconds_DefaultValue_ShouldBe30()
    {
        // Arrange.
        // Act.
        var config = new DashboardConfiguration();

        // Assert.
        config.RefreshIntervalSeconds.ShouldBe(30);
    }

}
