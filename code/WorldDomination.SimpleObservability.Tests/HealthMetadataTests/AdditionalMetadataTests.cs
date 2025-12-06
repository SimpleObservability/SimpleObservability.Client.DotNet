using Bogus;

namespace WorldDomination.SimpleObservability.Tests.HealthMetadataTests;

/// <summary>
/// Tests for the <see cref="HealthMetadata.AdditionalMetadata"/> property.
/// </summary>
public class AdditionalMetadataTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void AdditionalMetadata_WithMultipleEntries_ShouldStoreCorrectly()
    {
        // Arrange.
        var additionalMetadata = new Dictionary<string, string>
        {
            ["Database"] = "PostgreSQL",
            ["Cache"] = "Redis",
            ["Queue"] = "RabbitMQ",
            ["Storage"] = "Azure Blob"
        };

        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = _faker.System.Version().ToString(),
            AdditionalMetadata = additionalMetadata
        };

        // Assert.
        metadata.AdditionalMetadata.ShouldNotBeNull();
        metadata.AdditionalMetadata.Count.ShouldBe(4);
        metadata.AdditionalMetadata["Database"].ShouldBe("PostgreSQL");
        metadata.AdditionalMetadata["Cache"].ShouldBe("Redis");
        metadata.AdditionalMetadata["Queue"].ShouldBe("RabbitMQ");
        metadata.AdditionalMetadata["Storage"].ShouldBe("Azure Blob");
    }

    [Fact]
    public void AdditionalMetadata_WithNullValue_ShouldBeNull()
    {
        // Arrange.
        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = _faker.System.Version().ToString()
        };

        // Assert.
        metadata.AdditionalMetadata.ShouldBeNull();
    }

    [Fact]
    public void AdditionalMetadata_WithEmptyDictionary_ShouldBeEmpty()
    {
        // Arrange.
        var emptyMetadata = new Dictionary<string, string>();

        // Act.
        var metadata = new HealthMetadata
        {
            ServiceName = _faker.Company.CompanyName(),
            Version = _faker.System.Version().ToString(),
            AdditionalMetadata = emptyMetadata
        };

        // Assert.
        metadata.AdditionalMetadata.ShouldNotBeNull();
        metadata.AdditionalMetadata.ShouldBeEmpty();
    }
}
