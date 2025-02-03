using NotificareSdk.Geo.Core.Events;
using NotificareSdk.Geo.Core.Internal;
using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo;

public static class NotificareGeo
{
    private static readonly Lazy<INotificareGeoPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificareGeoPlatform Platform
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


    public static event EventHandler<NotificareLocationUpdatedEventArgs> LocationUpdated
    {
        add => Platform.LocationUpdated += value;
        remove => Platform.LocationUpdated -= value;
    }

    public static event EventHandler<NotificareRegionEnteredEventArgs> RegionEntered
    {
        add => Platform.RegionEntered += value;
        remove => Platform.RegionEntered -= value;
    }

    public static event EventHandler<NotificareRegionExitedEventArgs> RegionExited
    {
        add => Platform.RegionExited += value;
        remove => Platform.RegionExited -= value;
    }

    public static event EventHandler<NotificareBeaconEnteredEventArgs> BeaconEntered
    {
        add => Platform.BeaconEntered += value;
        remove => Platform.BeaconEntered -= value;
    }

    public static event EventHandler<NotificareBeaconExitedEventArgs> BeaconExited
    {
        add => Platform.BeaconExited += value;
        remove => Platform.BeaconExited -= value;
    }

    public static event EventHandler<NotificareBeaconsRangedEventArgs> BeaconsRanged
    {
        add => Platform.BeaconsRanged += value;
        remove => Platform.BeaconsRanged -= value;
    }

    public static event EventHandler<NotificareVisitEventArgs> Visit
    {
        add => Platform.Visit += value;
        remove => Platform.Visit -= value;
    }

    public static event EventHandler<NotificareHeadingUpdatedEventArgs> HeadingUpdated
    {
        add => Platform.HeadingUpdated += value;
        remove => Platform.HeadingUpdated -= value;
    }


    public static bool HasLocationServicesEnabled => Platform.HasLocationServicesEnabled;

    public static bool HasBluetoothEnabled => Platform.HasBluetoothEnabled;

    public static IList<NotificareRegion> MonitoredRegions => Platform.MonitoredRegions;

    public static IList<NotificareRegion> EnteredRegions => Platform.EnteredRegions;

    public static void EnableLocationUpdates() => Platform.EnableLocationUpdates();

    public static void DisableLocationUpdates() => Platform.DisableLocationUpdates();


    private static INotificareGeoPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificareGeoPlatformAndroid();
#elif IOS
        return new iOS.NotificareGeoPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}
