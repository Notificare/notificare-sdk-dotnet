using NotificareSdk;
using NotificareSdk.Core.Events;
using Sample.Pages.Home.Views;

namespace Sample.Pages.Home;

public partial class HomePage : ContentPage
{
    private VerticalStackLayout? _dynamicViews;

    public HomePage()
    {
        InitializeComponent();

        if (Notificare.IsReady)
        {
            AddHomeCardViews();
        }

        Notificare.Ready += NotificareOnReady;
        Notificare.Unlaunched += NotificareOnUnlaunched;
    }

    private void AddHomeCardViews()
    {
        if (_dynamicViews != null) return;

        _dynamicViews = new VerticalStackLayout
        {
            Margin = new Thickness(0, 12),
            Spacing = 26,
            Children =
            {
                new DeviceCardView(),
                new RemoteNotificationsCardView(),
                new DoNotDisturbCardView(),
                new GeoCardView(),
                new InAppMessagingCardView(),
                new OtherFeaturesCardView()
            }
        };

        HomeCardsView.Children.Add(_dynamicViews);
    }

    private void RemoveHomeCardViews()
    {
        if (_dynamicViews == null) return;

        HomeCardsView.Children.Remove(_dynamicViews);
        _dynamicViews = null;
    }

    private void NotificareOnReady(object? sender, NotificareReadyEventArgs e)
    {
        AddHomeCardViews();
    }

    private void NotificareOnUnlaunched(object? sender, NotificareUnlaunchedEventArgs e)
    {
        RemoveHomeCardViews();
    }
}