using CommunityToolkit.Mvvm.ComponentModel;
using NotificareSdk.Geo;
using NotificareSdk.Geo.Core.Events;
using NotificareSdk.Geo.Core.Models;

namespace Sample.ViewModels;

public partial class BeaconsViewModel : ObservableObject
{
    [ObservableProperty] private  IList<NotificareBeacon> _beacons;

    public BeaconsViewModel()
    {
        NotificareGeo.BeaconsRanged += OnBeaconsRanged;
    }

    private void OnBeaconsRanged(object? sender, NotificareBeaconsRangedEventArgs e)
    {
        Beacons = e.Beacons;
    }
}