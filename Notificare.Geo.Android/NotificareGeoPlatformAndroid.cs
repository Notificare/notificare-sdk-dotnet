using Android.Content;
using NotificareSdk.Geo.Android.Internal;
using NotificareSdk.Geo.Core.Events;
using NotificareSdk.Geo.Core.Internal;
using NotificareSdk.Geo.Core.Models;
using NativeNotificare = NotificareSdk.Geo.Android.Binding.NotificareGeoCompat;

namespace NotificareSdk.Geo.Android;

public class NotificareGeoPlatformAndroid : INotificareGeoPlatform
{
    public void Initialize()
    {
        NotificareDotNetGeoIntentReceiver.Platform = this;
        NativeNotificare.IntentReceiver = Java.Lang.Class.FromType(typeof(NotificareDotNetGeoIntentReceiver));
    }

    public event EventHandler<NotificareLocationUpdatedEventArgs>? LocationUpdated;
    public event EventHandler<NotificareRegionEnteredEventArgs>? RegionEntered;
    public event EventHandler<NotificareRegionExitedEventArgs>? RegionExited;
    public event EventHandler<NotificareBeaconEnteredEventArgs>? BeaconEntered;
    public event EventHandler<NotificareBeaconExitedEventArgs>? BeaconExited;
    public event EventHandler<NotificareBeaconsRangedEventArgs>? BeaconsRanged;
    public event EventHandler<NotificareVisitEventArgs>? Visit;
    public event EventHandler<NotificareHeadingUpdatedEventArgs>? HeadingUpdated;

    public bool HasLocationServicesEnabled => NativeNotificare.HasLocationServicesEnabled;

    public bool HasBluetoothEnabled => NativeNotificare.HasBluetoothEnabled;

    public IList<NotificareRegion> MonitoredRegions =>
        NativeNotificare.MonitoredRegions.Select(NativeConverter.FromNativeRegion).ToList();

    public IList<NotificareRegion> EnteredRegions =>
        NativeNotificare.EnteredRegions.Select(NativeConverter.FromNativeRegion).ToList();

    public void EnableLocationUpdates()
    {
        NativeNotificare.EnableLocationUpdates();
    }

    public void DisableLocationUpdates()
    {
        NativeNotificare.DisableLocationUpdates();
    }


    [BroadcastReceiver(Enabled = true, Exported = false)]
    private class NotificareDotNetGeoIntentReceiver : Binding.NotificareGeoIntentReceiver
    {
        internal static NotificareGeoPlatformAndroid? Platform;

        protected override void OnLocationUpdated(Context context, Binding.Models.NotificareLocation location)
        {
            Platform?.LocationUpdated?.Invoke(
                this,
                new NotificareLocationUpdatedEventArgs(
                    NativeConverter.FromNativeLocation(location)
                )
            );
        }

        protected override void OnRegionEntered(Context context, Binding.Models.NotificareRegion region)
        {
            Platform?.RegionEntered?.Invoke(
                this,
                new NotificareRegionEnteredEventArgs(
                    NativeConverter.FromNativeRegion(region)
                )
            );
        }

        protected override void OnRegionExited(Context context, Binding.Models.NotificareRegion region)
        {
            Platform?.RegionExited?.Invoke(
                this,
                new NotificareRegionExitedEventArgs(
                    NativeConverter.FromNativeRegion(region)
                )
            );
        }

        protected override void OnBeaconEntered(Context context, Binding.Models.NotificareBeacon beacon)
        {
            Platform?.BeaconEntered?.Invoke(
                this,
                new NotificareBeaconEnteredEventArgs(
                    NativeConverter.FromNativeBeacon(beacon)
                )
            );
        }

        protected override void OnBeaconExited(Context context, Binding.Models.NotificareBeacon beacon)
        {
            Platform?.BeaconExited?.Invoke(
                this,
                new NotificareBeaconExitedEventArgs(
                    NativeConverter.FromNativeBeacon(beacon)
                )
            );
        }

        protected override void OnBeaconsRanged(
            Context context,
            Binding.Models.NotificareRegion region,
            IList<Binding.Models.NotificareBeacon> beacons
        )
        {
            Platform?.BeaconsRanged?.Invoke(
                this,
                new NotificareBeaconsRangedEventArgs(
                    NativeConverter.FromNativeRegion(region),
                    beacons.Select(NativeConverter.FromNativeBeacon).ToList()
                )
            );
        }
    }
}
