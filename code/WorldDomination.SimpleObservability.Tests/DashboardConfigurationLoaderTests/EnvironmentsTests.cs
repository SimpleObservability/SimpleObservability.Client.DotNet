namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

public class EnvironmentsTests
{
    [Fact]
    public void Environments_WithNoServices_ShouldReturnEmptyList()
    {
        // Arrange.
        var config = new DashboardConfiguration();

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.ShouldBeEmpty();
    }

    [Fact]
    public void Environments_WithMultipleServices_ShouldReturnUniqueEnvironments()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "DEV", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "DEV", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "UAT", HealthCheckUrl = "http://localhost:5003" },
                new() { Name = "Service 4", Environment = "PROD", HealthCheckUrl = "http://localhost:5004" }
            ]
        };

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.Count.ShouldBe(3);
        environments.ShouldContain("DEV");
        environments.ShouldContain("UAT");
        environments.ShouldContain("PROD");
    }

    [Fact]
    public void Environments_WithNoEnvironmentOrder_ShouldReturnAlphabeticallySorted()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "PROD", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "DEV", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "UAT", HealthCheckUrl = "http://localhost:5003" }
            ]
        };

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.Count.ShouldBe(3);
        environments[0].ShouldBe("DEV");
        environments[1].ShouldBe("PROD");
        environments[2].ShouldBe("UAT");
    }

    [Fact]
    public void Environments_WithEnvironmentOrder_ShouldRespectOrder()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "PROD", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "DEV", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "UAT", HealthCheckUrl = "http://localhost:5003" }
            ],
            EnvironmentOrder = ["PROD", "UAT", "DEV"]
        };

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.Count.ShouldBe(3);
        environments[0].ShouldBe("PROD");
        environments[1].ShouldBe("UAT");
        environments[2].ShouldBe("DEV");
    }

    [Fact]
    public void Environments_WithPartialEnvironmentOrder_ShouldPlaceUnorderedLast()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "PROD", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "DEV", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "UAT", HealthCheckUrl = "http://localhost:5003" },
                new() { Name = "Service 4", Environment = "STAGING", HealthCheckUrl = "http://localhost:5004" }
            ],
            EnvironmentOrder = ["PROD", "UAT"]
        };

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.Count.ShouldBe(4);
        environments[0].ShouldBe("PROD");
        environments[1].ShouldBe("UAT");
        environments[2].ShouldBe("DEV");
        environments[3].ShouldBe("STAGING");
    }

    [Fact]
    public void Environments_WithEmptyEnvironmentOrder_ShouldReturnAlphabeticallySorted()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "PROD", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "DEV", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "UAT", HealthCheckUrl = "http://localhost:5003" }
            ],
            EnvironmentOrder = []
        };

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.Count.ShouldBe(3);
        environments[0].ShouldBe("DEV");
        environments[1].ShouldBe("PROD");
        environments[2].ShouldBe("UAT");
    }

    [Fact]
    public void Environments_WithDuplicateEnvironments_ShouldReturnUniqueOnly()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "DEV", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "DEV", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "DEV", HealthCheckUrl = "http://localhost:5003" }
            ]
        };

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.Count.ShouldBe(1);
        environments[0].ShouldBe("DEV");
    }

    [Fact]
    public void Environments_WhenAccessedMultipleTimes_ShouldReturnCachedInstance()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "PROD", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "DEV", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "UAT", HealthCheckUrl = "http://localhost:5003" }
            ]
        };

        // Act.
        var environments1 = config.Environments;
        var environments2 = config.Environments;

        // Assert.
        ReferenceEquals(environments1, environments2).ShouldBeTrue();
    }

    [Fact]
    public void Environments_WithCaseInsensitiveDuplicates_ShouldReturnUniqueOnly()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "dev", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "DEV", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "Dev", HealthCheckUrl = "http://localhost:5003" }
            ]
        };

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.Count.ShouldBe(1);
    }

    [Fact]
    public void Environments_WithCaseInsensitiveEnvironmentOrder_ShouldMatchCorrectly()
    {
        // Arrange.
        var config = new DashboardConfiguration
        {
            Services =
            [
                new() { Name = "Service 1", Environment = "PROD", HealthCheckUrl = "http://localhost:5001" },
                new() { Name = "Service 2", Environment = "dev", HealthCheckUrl = "http://localhost:5002" },
                new() { Name = "Service 3", Environment = "UAT", HealthCheckUrl = "http://localhost:5003" }
            ],
            EnvironmentOrder = ["prod", "uat", "DEV"]
        };

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.Count.ShouldBe(3);
        environments[0].ShouldBe("PROD");
        environments[1].ShouldBe("UAT");
        environments[2].ShouldBe("dev");
    }

    [Fact]
    public void Environments_WhenServicesBackingFieldIsNull_ShouldReturnEmptyList()
    {
        // Arrange.
        var config = TestHelpers.CreateConfigurationWithNullServices();

        // Act.
        var environments = config.Environments;

        // Assert.
        environments.ShouldNotBeNull();
        environments.ShouldBeEmpty();
    }


    [Fact]
    public void Environments_WhenServicesBackingFieldIsNull_ShouldBeCached()
    {
        // Arrange.
        var config = TestHelpers.CreateConfigurationWithNullServices();

        // Act.
        var environments1 = config.Environments;
        var environments2 = config.Environments;

        // Assert.
        environments1.ShouldNotBeNull();
        environments2.ShouldNotBeNull();
        ReferenceEquals(environments1, environments2).ShouldBeTrue();
    }
}
