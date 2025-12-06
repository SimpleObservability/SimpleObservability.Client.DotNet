using System.Reflection;
using System.Runtime.CompilerServices;

namespace WorldDomination.SimpleObservability.Tests.DashboardConfigurationLoaderTests;

internal static class TestHelpers
{
    internal static DashboardConfiguration CreateConfigurationWithNullServices()
    {
        // WOW: Using RuntimeHelpers to create an instance without invoking the constructor.
        var config = (DashboardConfiguration)RuntimeHelpers.GetUninitializedObject(typeof(DashboardConfiguration));

        var backingField = typeof(DashboardConfiguration).GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        backingField?.SetValue(config, null);

        return config;
    }
}
