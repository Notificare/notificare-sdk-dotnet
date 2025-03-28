using NotificareSdk.Core.Internal;

namespace NotificareSdk;

public class NotificareEventsModule
{
    private readonly INotificarePlatform _platform;

    internal NotificareEventsModule(INotificarePlatform platform)
    {
        _platform = platform;
    }

    /// <summary>
    /// Logs in Notificare a custom event in the application.
    ///
    /// This function allows logging, in Notificare, of application-specific events, optionally associating structured
    /// data for more detailed event tracking and analysis.
    /// </summary>
    /// <param name="eventName">The name of the custom event to log.</param>
    /// <param name="data">Optional structured event data for further details.</param>
    /// <returns>
    /// A task that resolves when the custom event has been successfully logged.
    /// </returns>
    public Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null) =>
        _platform.LogCustomAsync(eventName, data);
}
