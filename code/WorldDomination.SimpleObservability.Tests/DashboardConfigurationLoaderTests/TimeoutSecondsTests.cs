namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

public class TimeoutSecondsTests
{
    [Fact]
    public void TimeoutSeconds_DefaultValue_ShouldBe5()
    {
        // Arrange.
        // Act.
        var config = new DashboardConfiguration();

        // Assert.
        config.TimeoutSeconds.ShouldBe(5);
    }
}
