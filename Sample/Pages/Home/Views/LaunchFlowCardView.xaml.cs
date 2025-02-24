using NotificareSdk;

namespace Sample.Pages.Home.Views;

public partial class LaunchFlowCardView : ContentView
{
    public LaunchFlowCardView()
    {
        InitializeComponent();
    }

    private void ShowNotificareStatusInfo(object sender, EventArgs args)
    {
        var isReady = Notificare.IsReady;
        var isConfigured = Notificare.IsConfigured;
        
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Shell.Current.DisplayAlert(
                title: "Notificare Status",
                message: $"Ready: {isReady}\nConfigured: {isConfigured}",
                cancel: "OK"
            );
        });
    }
}
