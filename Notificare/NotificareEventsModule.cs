using NotificareSdk.Core.Internal;

namespace NotificareSdk;

public class NotificareEventsModule
{
    private readonly INotificarePlatform _platform;

    internal NotificareEventsModule(INotificarePlatform platform)
    {
        _platform = platform;
    }

    public Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null) =>
        _platform.LogCustomAsync(eventName, data);
}
