using NotificareSdk.Geo.Core.Events;
using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.Core.Internal;

public interface INotificareGeoPlatform
{
    void Initialize();

    event EventHandler<NotificareLocationUpdatedEventArgs> LocationUpdated;

    event EventHandler<NotificareRegionEnteredEventArgs> RegionEntered;

    event EventHandler<NotificareRegionExitedEventArgs> RegionExited;

    event EventHandler<NotificareBeaconEnteredEventArgs> BeaconEntered;

    event EventHandler<NotificareBeaconExitedEventArgs> BeaconExited;

    event EventHandler<NotificareBeaconsRangedEventArgs> BeaconsRanged;

    event EventHandler<NotificareVisitEventArgs> Visit;

    event EventHandler<NotificareHeadingUpdatedEventArgs> HeadingUpdated;

    bool HasLocationServicesEnabled { get; }

    bool HasBluetoothEnabled { get; }

    IList<NotificareRegion> MonitoredRegions { get; }

    IList<NotificareRegion> EnteredRegions { get; }

    void EnableLocationUpdates();

    void DisableLocationUpdates();
}
