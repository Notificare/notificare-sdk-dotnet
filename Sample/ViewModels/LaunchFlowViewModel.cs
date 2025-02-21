using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotificareSdk;
using NotificareSdk.Core.Events;

namespace Sample.ViewModels;

public partial class LaunchFlowViewModel : ObservableObject
{
    [ObservableProperty] private bool _isReady;

    public LaunchFlowViewModel()
    {
        IsReady = Notificare.IsReady;

        Notificare.Ready += OnReady;
        Notificare.Unlaunched += OnUnlaunch;
    }

    private void OnReady(object? sender, NotificareReadyEventArgs e)
    {
        IsReady = true;
    }

    private void OnUnlaunch(object? sender, NotificareUnlaunchedEventArgs e)
    {
        IsReady = false;
    }

    [RelayCommand]
    private async Task Launch()
    {
        try
        {
            await Notificare.LaunchAsync();
            Console.WriteLine("Launch success.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Launch failed: {exception}");
        }
    }

    [RelayCommand]
    private async Task Unlaunch()
    {
        try
        {
            await Notificare.UnlaunchAsync();
            Console.WriteLine("Unlaunch success.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Unlaunch failed: {exception}");
        }
    }
}