using System.Reflection;
using System.Runtime.CompilerServices;

namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

public class ServicesTests
{
    [Fact]
    public void Services_WhenBackingFieldIsNull_ShouldReturnEmptyList()
    {
        // Arrange.
        var config =TestHelpers. CreateConfigurationWithNullServices();

        // Act.
        var services = config.Services;

        // Assert.
        services.ShouldNotBeNull();
        services.ShouldBeEmpty();
    }

    [Fact]
    public void Services_WhenBackingFieldIsNullAndAccessedMultipleTimes_ShouldReturnSameInstance()
    {
        // Arrange.
        var config =TestHelpers. CreateConfigurationWithNullServices();

        // Act.
        var services1 = config.Services;
        var services2 = config.Services;

        // Assert.
        services1.ShouldNotBeNull();
        services2.ShouldNotBeNull();
        ReferenceEquals(services1, services2).ShouldBeTrue();
    }

    [Fact]
    public void Services_WhenSetToEmptyList_ShouldReturnEmptyList()
    {
        // Arrange.
        var config = new DashboardConfiguration();

        // Act.
        var services = config.Services;

        // Assert.
        services.ShouldNotBeNull();
        services.ShouldBeEmpty();
    }

    [Fact]
    public void Services_WhenSetToPopulatedList_ShouldReturnSameList()
    {
        // Arrange.
        var expectedServices = new List<ServiceEndpoint>
        {
            new() { Name = "Service 1", Environment = "DEV", HealthCheckUrl = "http://localhost:5001" },
            new() { Name = "Service 2", Environment = "PROD", HealthCheckUrl = "http://localhost:5002" }
        };

        var config = new DashboardConfiguration
        {
            Services = expectedServices
        };

        // Act.
        var services = config.Services;

        // Assert.
        services.ShouldBe(expectedServices);
        services.Count.ShouldBe(2);
    }

    [Fact]
    public void Services_WhenBackingFieldIsNull_ShouldNotThrowExceptionOnAccess()
    {
        // Arrange.
        var config =TestHelpers. CreateConfigurationWithNullServices();

        // Act.
        var exception = Record.Exception(() =>
        {
            var services = config.Services;
            var count = services.Count;
            var any = services.Any();
        });

        // Assert.
        exception.ShouldBeNull();
    }

    [Fact]
    public void Services_WhenBackingFieldIsNull_CanIterateSafely()
    {
        // Arrange.
        var config =TestHelpers. CreateConfigurationWithNullServices();

        // Act.
        var exception = Record.Exception(() =>
        {
            foreach (var service in config.Services)
            {
                // This should not throw or execute.
            }
        });

        // Assert.
        exception.ShouldBeNull();
    }

    [Fact]
    public void Services_WhenBackingFieldIsNull_CanUseLinqSafely()
    {
        // Arrange.
        var config =TestHelpers. CreateConfigurationWithNullServices();

        // Act.
        var exception = Record.Exception(() =>
        {
            var services = config.Services;
            var count = services.Count;
            var any = services.Count != 0;
            var names = services.Select(s => s.Name).ToList();
        });

        // Assert.
        exception.ShouldBeNull();
    }

    [Fact]
    public void Services_AfterLazyInitialization_CanBeModified()
    {
        // Arrange.
        var config =TestHelpers.CreateConfigurationWithNullServices();

        // Act.
        var services = config.Services;
        services.Add(new ServiceEndpoint
        {
            Name = "New Service",
            Environment = "DEV",
            HealthCheckUrl = "http://localhost:5001"
        });

        // Assert.
        services.Count.ShouldBe(1);
        config.Services.Count.ShouldBe(1);
    }

}
