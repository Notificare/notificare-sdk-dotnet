using NotificareSdk.Core.Events;
using NotificareSdk.Core.Internal;
using NotificareSdk.Core.Models;

namespace NotificareSdk;

public static class Notificare
{
    private static readonly Lazy<INotificarePlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificarePlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }


    public static event EventHandler<NotificareReadyEventArgs> Ready
    {
        add => Platform.Ready += value;
        remove => Platform.Ready -= value;
    }

    public static event EventHandler<NotificareUnlaunchedEventArgs> Unlaunched
    {
        add => Platform.Unlaunched += value;
        remove => Platform.Unlaunched -= value;
    }

    public static event EventHandler<NotificareDeviceRegisteredEventArgs> DeviceRegistered
    {
        add => Platform.DeviceRegistered += value;
        remove => Platform.DeviceRegistered -= value;
    }

    // event EventHandler<...>? UrlOpened


    public static NotificareDeviceModule Device { get; } = new(Platform);

    public static NotificareEventsModule Events { get; } = new(Platform);


    public static bool IsConfigured => Platform.IsConfigured;

    public static bool IsReady => Platform.IsReady;

    public static NotificareApplication? Application => Platform.Application;


    public static Task LaunchAsync() => Platform.LaunchAsync();

    public static Task UnlaunchAsync() => Platform.UnlaunchAsync();

    public static Task<NotificareApplication> FetchApplicationAsync() => Platform.FetchApplicationAsync();

    public static Task<NotificareNotification> FetchNotificationAsync(string id) => Platform.FetchNotificationAsync(id);

    public static Task<NotificareDynamicLink> FetchDynamicLinkAsync(string url) => Platform.FetchDynamicLinkAsync(url);

    public static Task<bool> CanEvaluateDeferredLinkAsync() => Platform.CanEvaluateDeferredLinkAsync();

    public static Task<bool> EvaluateDeferredLinkAsync() => Platform.EvaluateDeferredLinkAsync();


    private static INotificarePlatform CreateNotificare()
    {
#if __IOS__
        return new iOS.NotificarePlatformIos();
#elif __ANDROID__
        return new Android.NotificarePlatformAndroid();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}
