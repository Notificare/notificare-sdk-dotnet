using CommunityToolkit.Mvvm.ComponentModel;
using NotificareSdk.Geo;

namespace Sample.ViewModels;

public partial class GeoViewModel : ObservableObject
{
    [ObservableProperty] private bool _hasLocationEnabled;

    public GeoViewModel()
    {
        UpdateLocationUpdatesStatus();
    }

    public void EnabledLocationUpdates()
    {
        NotificareGeo.EnableLocationUpdates();
        UpdateLocationUpdatesStatus();
    }

    public void DisabledLocationUpdates()
    {
        NotificareGeo.DisableLocationUpdates();
        UpdateLocationUpdatesStatus();
    }

    private void UpdateLocationUpdatesStatus()
    {
        HasLocationEnabled = NotificareGeo.HasLocationServicesEnabled;
    }
}