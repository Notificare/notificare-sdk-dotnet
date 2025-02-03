using NotificareSdk.Geo.Core.Events;
using NotificareSdk.Geo.Core.Internal;
using NotificareSdk.Geo.Core.Models;
using NotificareSdk.Geo.iOS.Internal;

namespace NotificareSdk.Geo.iOS;

public class NotificareGeoPlatformIos : INotificareGeoPlatform
{
    private InternalNotificareGeoDelegate? _delegate;
    private Binding.NotificareGeoNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalNotificareGeoDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<NotificareLocationUpdatedEventArgs>? LocationUpdated;
    public event EventHandler<NotificareRegionEnteredEventArgs>? RegionEntered;
    public event EventHandler<NotificareRegionExitedEventArgs>? RegionExited;
    public event EventHandler<NotificareBeaconEnteredEventArgs>? BeaconEntered;
    public event EventHandler<NotificareBeaconExitedEventArgs>? BeaconExited;
    public event EventHandler<NotificareBeaconsRangedEventArgs>? BeaconsRanged;
    public event EventHandler<NotificareVisitEventArgs>? Visit;
    public event EventHandler<NotificareHeadingUpdatedEventArgs>? HeadingUpdated;

    public bool HasLocationServicesEnabled => _native.HasLocationServicesEnabled;

    public bool HasBluetoothEnabled => _native.HasBluetoothEnabled;

    public IList<NotificareRegion> MonitoredRegions =>
        _native.MonitoredRegions.Select(NativeConverter.FromNativeRegion).ToList();

    public IList<NotificareRegion> EnteredRegions =>
        _native.EnteredRegions.Select(NativeConverter.FromNativeRegion).ToList();

    public void EnableLocationUpdates()
    {
        _native.EnableLocationUpdates();
    }

    public void DisableLocationUpdates()
    {
        _native.DisableLocationUpdates();
    }


    private sealed class InternalNotificareGeoDelegate : Binding.NotificareGeoNativeBindingDelegate
    {
        private readonly NotificareGeoPlatformIos _platform;

        internal InternalNotificareGeoDelegate(NotificareGeoPlatformIos platform)
        {
            _platform = platform;
        }


        public override void DidUpdateLocations(
            Binding.NotificareGeoNativeBinding notificareGeo,
            Binding.NotificareLocation[] locations
        )
        {
            if (locations.Length == 0) return;

            _platform.LocationUpdated?.Invoke(
                _platform,
                new NotificareLocationUpdatedEventArgs(
                    NativeConverter.FromNativeLocation(locations[0])
                )
            );
        }

        // public override void DidFailWith(
        //     Binding.NotificareGeoNativeBinding notificareGeo,
        //     NSError error
        // )
        // {
        //     
        // }

        // public override void DidStartMonitoringForRegion(
        //     Binding.NotificareGeoNativeBinding notificareGeo,
        //     Binding.NotificareRegion region
        // )
        // {
        // }

        // public override void DidStartMonitoringForBeacon(
        //     Binding.NotificareGeoNativeBinding notificareGeo,
        //     Binding.NotificareBeacon beacon
        // )
        // {
        // }

        // public override void MonitoringDidFailForRegion(
        //     Binding.NotificareGeoNativeBinding notificareGeo,
        //     Binding.NotificareRegion region,
        //     NSError error
        // )
        // {
        // }

        // public override void MonitoringDidFailForBeacon(
        //     Binding.NotificareGeoNativeBinding notificareGeo,
        //     Binding.NotificareBeacon beacon,
        //     NSError error
        // )
        // {
        // }

        // public override void DidDetermineState(
        //     Binding.NotificareGeoNativeBinding notificareGeo,
        //     CoreLocation.CLRegionState state,
        //     NotificareRegion region
        // )
        // {
        // }

        // public override void DidDetermineState(
        //     Binding.NotificareGeoNativeBinding notificareGeo,
        //     CoreLocation.CLRegionState state,
        //     Binding.NotificareBeacon beacon
        // )
        // {
        // }

        public override void DidEnterRegion(
            Binding.NotificareGeoNativeBinding notificareGeo,
            Binding.NotificareRegion region
        )
        {
            _platform.RegionEntered?.Invoke(
                _platform,
                new NotificareRegionEnteredEventArgs(
                    NativeConverter.FromNativeRegion(region)
                )
            );
        }

        public override void DidEnterBeacon(
            Binding.NotificareGeoNativeBinding notificareGeo,
            Binding.NotificareBeacon beacon
        )
        {
            _platform.BeaconEntered?.Invoke(
                _platform,
                new NotificareBeaconEnteredEventArgs(
                    NativeConverter.FromNativeBeacon(beacon)
                )
            );
        }

        public override void DidExitRegion(
            Binding.NotificareGeoNativeBinding notificareGeo,
            Binding.NotificareRegion region
        )
        {
            _platform.RegionExited?.Invoke(
                _platform,
                new NotificareRegionExitedEventArgs(
                    NativeConverter.FromNativeRegion(region)
                )
            );
        }

        public override void DidExitBeacon(
            Binding.NotificareGeoNativeBinding notificareGeo,
            Binding.NotificareBeacon beacon
        )
        {
            _platform.BeaconExited?.Invoke(
                _platform,
                new NotificareBeaconExitedEventArgs(
                    NativeConverter.FromNativeBeacon(beacon)
                )
            );
        }

        public override void DidVisit(
            Binding.NotificareGeoNativeBinding notificareGeo,
            Binding.NotificareVisit visit
        )
        {
            _platform.Visit?.Invoke(
                _platform,
                new NotificareVisitEventArgs(
                    NativeConverter.FromNativeVisit(visit)
                )
            );
        }

        public override void DidUpdateHeading(
            Binding.NotificareGeoNativeBinding notificareGeo,
            Binding.NotificareHeading heading
        )
        {
            _platform.HeadingUpdated?.Invoke(
                _platform,
                new NotificareHeadingUpdatedEventArgs(
                    NativeConverter.FromNativeHeading(heading)
                )
            );
        }

        public override void DidRange(
            Binding.NotificareGeoNativeBinding notificareGeo,
            Binding.NotificareBeacon[] beacons,
            Binding.NotificareRegion region
        )
        {
            _platform.BeaconsRanged?.Invoke(
                _platform,
                new NotificareBeaconsRangedEventArgs(
                    NativeConverter.FromNativeRegion(region),
                    beacons.Select(NativeConverter.FromNativeBeacon).ToList()
                )
            );
        }

        // public override void DidFailRangingFor(
        //     Binding.NotificareGeoNativeBinding notificareGeo,
        //     Binding.NotificareRegion region,
        //     NSError error
        // )
        // {
        // }
    }
}
