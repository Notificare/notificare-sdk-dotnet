using CommunityToolkit.Mvvm.ComponentModel;
using NotificareSdk;
using NotificareSdk.Core.Models;


namespace Sample.ViewModels;

public partial class DoNotDisturbViewModel : ObservableObject
{
    [ObservableProperty] private bool _hasDnDEnabled;

    public DoNotDisturbViewModel()
    {
        CheckDoNotDisturbStatus();
    }

    public void ClearDoNotDisturb()
    {
        Task.Run(async () =>
        {
            try
            {
                Console.WriteLine("Check DnD here");
                await Notificare.Device.ClearDoNotDisturbAsync();

                Console.WriteLine("DnD cleared successfully.");
            }
            catch (Exception e)
            {
                HasDnDEnabled = true;

                Console.WriteLine($"Failed to clear DnD: {e.Message}");
            }
        });
    }

    public void UpdateDoNotDisturb()
    {
        Task.Run(async () =>
        {
            try
            {
                var startTime = new NotificareTime(23, 00);
                var endTime = new NotificareTime(08, 00);
                var dnd = new NotificareDoNotDisturb(startTime, endTime);

                await Notificare.Device.UpdateDoNotDisturbAsync(dnd);

                Console.WriteLine("DnD updated successfully.");
            }
            catch (Exception e)
            {
                HasDnDEnabled = false;

                Console.WriteLine($"Failed to update DnD: {e.Message}");
            }
        });
    }

    private void CheckDoNotDisturbStatus()
    {
        Task.Run(async () =>
        {
            try
            {
                var dnd = await Notificare.Device.FetchDoNotDisturbAsync();
                HasDnDEnabled = dnd != null;
            }
            catch (Exception e)
            {
                HasDnDEnabled = false;

                Console.WriteLine($"Failed to fetch DnD: {e.Message}");
            }
        });
    }
}
