namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

public class DefaultSectionNameTests
{
    [Fact]
    public void DefaultSectionName_ShouldBeDashboard()
    {
        // Arrange.
        // Act.
        var sectionName = DashboardConfigurationLoader.DefaultSectionName;

        // Assert.
        sectionName.ShouldBe("Dashboard");
    }
}
