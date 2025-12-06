using Microsoft.Extensions.Configuration;

namespace WorldDomination.SimpleObservability.Tests;

/// <summary>
/// Tests for the <see cref="DashboardConfigurationLoader.AddDashboardSettingsFile"/> method.
/// </summary>
public class AddDashboardSettingsFileTests
{
    [Fact]
    public void AddDashboardSettingsFile_WithNullBuilder_ShouldThrowArgumentNullException()
    {
        // Arrange.
        IConfigurationBuilder builder = null!;

        // Act.
        var exception = Should.Throw<ArgumentNullException>(() => builder.AddDashboardSettingsFile());

        // Assert.
        exception.ParamName.ShouldBe("builder");
    }

    [Fact]
    public void AddDashboardSettingsFile_WithDefaultParameters_ShouldReturnBuilder()
    {
        // Arrange.
        var builder = new ConfigurationBuilder();

        // Act.
        var result = builder.AddDashboardSettingsFile();

        // Assert.
        result.ShouldBe(builder);
        result.ShouldNotBeNull();
    }

    [Fact]
    public void AddDashboardSettingsFile_WithCustomFilename_ShouldReturnBuilder()
    {
        // Arrange.
        var builder = new ConfigurationBuilder();
        var customFilename = "custom-settings.json";

        // Act.
        var result = builder.AddDashboardSettingsFile(customFilename);

        // Assert.
        result.ShouldBe(builder);
        result.ShouldNotBeNull();
    }

    [Fact]
    public void AddDashboardSettingsFile_WithEnvironmentName_ShouldReturnBuilder()
    {
        // Arrange.
        var builder = new ConfigurationBuilder();

        // Act.
        var result = builder.AddDashboardSettingsFile(environmentName: "Development");

        // Assert.
        result.ShouldBe(builder);
        result.ShouldNotBeNull();
    }

    [Fact]
    public void AddDashboardSettingsFile_WithEnvironmentName_ShouldLoadEnvironmentSpecificFile()
    {
        // Arrange.
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);

        try
        {
            var baseFile = Path.Combine(tempPath, "dashboardsettings.json");
            var envFile = Path.Combine(tempPath, "dashboardsettings.Development.json");

            File.WriteAllText(baseFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 30
                  }
                }
                """);

            File.WriteAllText(envFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 60
                  }
                }
                """);

            var builder = new ConfigurationBuilder()
                .SetBasePath(tempPath)
                .AddDashboardSettingsFile(environmentName: "Development");

            // Act.
            var configuration = builder.Build();
            var config = configuration.Load();

            // Assert.
            config.RefreshIntervalSeconds.ShouldBe(60);
        }
        finally
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }

    [Fact]
    public void AddDashboardSettingsFile_WithEnvironmentName_ShouldMergeConfigurations()
    {
        // Arrange.
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);

        try
        {
            var baseFile = Path.Combine(tempPath, "dashboardsettings.json");
            var envFile = Path.Combine(tempPath, "dashboardsettings.Production.json");

            File.WriteAllText(baseFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 30,
                    "TimeoutSeconds": 5
                  }
                }
                """);

            File.WriteAllText(envFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 120
                  }
                }
                """);

            var builder = new ConfigurationBuilder()
                .SetBasePath(tempPath)
                .AddDashboardSettingsFile(environmentName: "Production");

            // Act.
            var configuration = builder.Build();
            var config = configuration.Load();

            // Assert.
            config.RefreshIntervalSeconds.ShouldBe(120);
            config.TimeoutSeconds.ShouldBe(5);
        }
        finally
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }

    [Fact]
    public void AddDashboardSettingsFile_WithMissingEnvironmentFile_ShouldOnlyLoadBaseFile()
    {
        // Arrange.
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);

        try
        {
            var baseFile = Path.Combine(tempPath, "dashboardsettings.json");

            File.WriteAllText(baseFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 45
                  }
                }
                """);

            var builder = new ConfigurationBuilder()
                .SetBasePath(tempPath)
                .AddDashboardSettingsFile(environmentName: "Staging");

            // Act.
            var configuration = builder.Build();
            var config = configuration.Load();

            // Assert.
            config.RefreshIntervalSeconds.ShouldBe(45);
        }
        finally
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }

    [Fact]
    public void AddDashboardSettingsFile_WithCustomFilenameAndEnvironment_ShouldLoadBothFiles()
    {
        // Arrange.
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);

        try
        {
            var baseFile = Path.Combine(tempPath, "custom.json");
            var envFile = Path.Combine(tempPath, "custom.Test.json");

            File.WriteAllText(baseFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 10
                  }
                }
                """);

            File.WriteAllText(envFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 20
                  }
                }
                """);

            var builder = new ConfigurationBuilder()
                .SetBasePath(tempPath)
                .AddDashboardSettingsFile(filename: "custom.json", environmentName: "Test");

            // Act.
            var configuration = builder.Build();
            var config = configuration.Load();

            // Assert.
            config.RefreshIntervalSeconds.ShouldBe(20);
        }
        finally
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }

    [Fact]
    public void AddDashboardSettingsFile_WithEmptyEnvironmentName_ShouldOnlyLoadBaseFile()
    {
        // Arrange.
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);

        try
        {
            var baseFile = Path.Combine(tempPath, "dashboardsettings.json");

            File.WriteAllText(baseFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 25
                  }
                }
                """);

            var builder = new ConfigurationBuilder()
                .SetBasePath(tempPath)
                .AddDashboardSettingsFile(environmentName: string.Empty);

            // Act.
            var configuration = builder.Build();
            var config = configuration.Load();

            // Assert.
            config.RefreshIntervalSeconds.ShouldBe(25);
        }
        finally
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }

    [Fact]
    public void AddDashboardSettingsFile_WithWhitespaceEnvironmentName_ShouldOnlyLoadBaseFile()
    {
        // Arrange.
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);

        try
        {
            var baseFile = Path.Combine(tempPath, "dashboardsettings.json");

            File.WriteAllText(baseFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 35
                  }
                }
                """);

            var builder = new ConfigurationBuilder()
                .SetBasePath(tempPath)
                .AddDashboardSettingsFile(environmentName: "   ");

            // Act.
            var configuration = builder.Build();
            var config = configuration.Load();

            // Assert.
            config.RefreshIntervalSeconds.ShouldBe(35);
        }
        finally
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }

    [Fact]
    public void AddDashboardSettingsFile_WithAllParameters_ShouldReturnBuilder()
    {
        // Arrange.
        var builder = new ConfigurationBuilder();

        // Act.
        var result = builder.AddDashboardSettingsFile(
            filename: "custom.json",
            optional: false,
            reloadOnChange: false,
            environmentName: "Development");

        // Assert.
        result.ShouldBe(builder);
        result.ShouldNotBeNull();
    }

    [Fact]
    public void AddDashboardSettingsFile_WithMultipleEnvironments_ShouldLoadLastEnvironmentValues()
    {
        // Arrange.
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);

        try
        {
            var baseFile = Path.Combine(tempPath, "dashboardsettings.json");
            var devFile = Path.Combine(tempPath, "dashboardsettings.Development.json");
            var prodFile = Path.Combine(tempPath, "dashboardsettings.Production.json");

            File.WriteAllText(baseFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 30,
                    "TimeoutSeconds": 5
                  }
                }
                """);

            File.WriteAllText(devFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 60
                  }
                }
                """);

            File.WriteAllText(prodFile, """
                {
                  "Dashboard": {
                    "RefreshIntervalSeconds": 120
                  }
                }
                """);

            var builder = new ConfigurationBuilder()
                .SetBasePath(tempPath)
                .AddDashboardSettingsFile(environmentName: "Development")
                .AddDashboardSettingsFile(environmentName: "Production");

            // Act.
            var configuration = builder.Build();
            var config = configuration.Load();

            // Assert.
            config.RefreshIntervalSeconds.ShouldBe(120);
            config.TimeoutSeconds.ShouldBe(5);
        }
        finally
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }

    [Fact]
    public void AddDashboardSettingsFile_WithComplexEnvironmentSpecificServices_ShouldMergeCorrectly()
    {
        // Arrange.
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);

        try
        {
            var baseFile = Path.Combine(tempPath, "dashboardsettings.json");
            var envFile = Path.Combine(tempPath, "dashboardsettings.Development.json");

            File.WriteAllText(baseFile, """
                {
                  "Dashboard": {
                    "Services": [
                      {
                        "Name": "Service 1",
                        "Environment": "PROD",
                        "HealthCheckUrl": "https://prod.example.com/healthz"
                      }
                    ],
                    "RefreshIntervalSeconds": 30
                  }
                }
                """);

            File.WriteAllText(envFile, """
                {
                  "Dashboard": {
                    "Services": [
                      {
                        "Name": "Service 1",
                        "Environment": "DEV",
                        "HealthCheckUrl": "http://localhost:5000/healthz"
                      }
                    ],
                    "RefreshIntervalSeconds": 5
                  }
                }
                """);

            var builder = new ConfigurationBuilder()
                .SetBasePath(tempPath)
                .AddDashboardSettingsFile(environmentName: "Development");

            // Act.
            var configuration = builder.Build();
            var config = configuration.Load();

            // Assert.
            config.Services.Count.ShouldBe(1);
            config.Services[0].Name.ShouldBe("Service 1");
            config.Services[0].Environment.ShouldBe("DEV");
            config.Services[0].HealthCheckUrl.ShouldBe("http://localhost:5000/healthz");
            config.RefreshIntervalSeconds.ShouldBe(5);
        }
        finally
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }
    }
}
