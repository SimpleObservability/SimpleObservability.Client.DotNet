namespace WorldDomination.SimpleObservability;

/// <summary>
/// Configuration for the Simple Observability dashboard.
/// </summary>
public record DashboardConfiguration
{
    private List<string>? _cachedEnvironments;
    private List<ServiceEndpoint>? _services;

    /// <summary>
    /// List of services to monitor. Each service will appear as a column in the dashboard. Required.
    /// The environments are automatically derived from the unique environment values in the services list.
    /// </summary>
    public List<ServiceEndpoint> Services
    {
        get => _services ??= [];
        init => _services = value;
    }

    /// <summary>
    /// The interval (in seconds) at which the dashboard should refresh health data. Optional, defaults to 30 seconds.
    /// </summary>
    public int RefreshIntervalSeconds { get; init; } = 30;

    /// <summary>
    /// Timeout (in seconds) for health check requests. Optional, defaults to 5 seconds.
    /// This is the default timeout; individual services can override this with their own timeout.
    /// </summary>
    public int TimeoutSeconds { get; init; } = 5;

    /// <summary>
    /// Optional ordered list of environment names. Environments will be displayed in this order.
    /// Any environments found in services but not listed here will appear last, sorted alphabetically.
    /// </summary>
    /// <example>["DEV", "UAT", "PROD"]</example>
    public List<string>? EnvironmentOrder { get; init; }

    /// <summary>
    /// Gets the list of unique environments from the services.
    /// Environments are ordered according to EnvironmentOrder if specified.
    /// Unlisted environments appear last in alphabetical order.
    /// </summary>
    public List<string> Environments
    {
        get
        {
            if (_cachedEnvironments is not null)
            {
                return _cachedEnvironments;
            }

            var allEnvironments = Services
                .Select(s => s.Environment)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (EnvironmentOrder is null || EnvironmentOrder.Count == 0)
            {
                _cachedEnvironments = allEnvironments
                    .OrderBy(e => e, StringComparer.OrdinalIgnoreCase)
                    .ToList();
                return _cachedEnvironments;
            }

            var orderedEnvironments = new List<string>(EnvironmentOrder.Count);
            var unorderedEnvironments = new List<string>();

            var orderLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < EnvironmentOrder.Count; i++)
            {
                orderLookup[EnvironmentOrder[i]] = i;
            }

            foreach (var env in allEnvironments)
            {
                if (orderLookup.ContainsKey(env))
                {
                    orderedEnvironments.Add(env);
                }
                else
                {
                    unorderedEnvironments.Add(env);
                }
            }

            orderedEnvironments.Sort((a, b) => orderLookup[a].CompareTo(orderLookup[b]));
            unorderedEnvironments.Sort(StringComparer.OrdinalIgnoreCase);

            _cachedEnvironments = [.. orderedEnvironments, .. unorderedEnvironments];

            return _cachedEnvironments;
        }
    }
}
