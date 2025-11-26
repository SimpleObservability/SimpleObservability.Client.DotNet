<h1 align="center">Simple: Observability Client : .NET </h1>

<div align="center">
  <i>A simple .NET client for your health endpoint</i>
</div>

<p align="center">

## Summary

A lightweight .NET library that provides a standardized schema for exposing service health information. Use this library to make your services compatible with the [Simple Observability](https://github.com/SimpleObservability) monitoring dashboard.

**Key Features:**
- 📊 Standard health metadata schema
- 🚀 Zero-configuration setup
- 🎯 Simple POCO classes
- ✅ Compatible with ASP.NET Core Minimal APIs and MVC
- 🔍 Optional additional metadata support

## Installation

```bash
dotnet add package WorldDomination.SimpleObservability
```

## Quick Start

### Basic Health Endpoint

Add a `/healthz` endpoint to your ASP.NET Core application:

```csharp
using WorldDomination.SimpleObservability;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/healthz", () =>
{
    var health = new HealthMetadata
    {
        ServiceName = "My API Service",
        Version = "1.0.0",
        Environment = "Production",
        Status = HealthStatus.Healthy
    };
    
    return Results.Json(health);
});

app.Run();
```

### Advanced Example with Additional Metadata

```csharp
app.MapGet("/healthz", () =>
{
    var health = new HealthMetadata
    {
        ServiceName = "My API Service",
        Version = "1.2.3",
        Environment = "Production",
        Status = HealthStatus.Healthy,
        Timestamp = DateTimeOffset.UtcNow,
        HostName = Environment.MachineName,
        Uptime = TimeSpan.FromHours(24),
        Description = "All systems operational",
        AdditionalMetadata = new Dictionary<string, string>
        {
            ["Database"] = "Connected",
            ["Cache"] = "Redis v7.0",
            ["Region"] = "us-west-2"
        }
    };
    
    return Results.Json(health);
});
```

## Health Status Values

- `HealthStatus.Healthy` - Service is operating normally.
- `HealthStatus.Degraded` - Service is operational but experiencing issues.
- `HealthStatus.Unhealthy` - Service is not operating correctly.

## JSON Response Example

```json
{
  "serviceName": "My API Service",
  "version": "1.2.3",
  "environment": "Production",
  "status": "Healthy",
  "timestamp": "2024-01-15T10:30:00Z",
  "hostName": "server-01",
  "uptime": "1.00:00:00",
  "description": "All systems operational",
  "additionalMetadata": {
    "database": "Connected",
    "cache": "Redis v7.0",
    "region": "us-west-2"
  }
}
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

See [LICENSE](LICENSE) for details.
