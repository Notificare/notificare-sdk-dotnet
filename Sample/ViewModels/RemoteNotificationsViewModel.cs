using CommunityToolkit.Mvvm.ComponentModel;
using NotificareSdk.Inbox;
using NotificareSdk.Inbox.Core.Events;
using NotificareSdk.Push;
using NotificareSdk.Push.Core.Events;

namespace Sample.ViewModels;

public partial class RemoteNotificationsViewModel : ObservableObject
{
    [ObservableProperty] private bool _hasNotificationsEnabled;
    [ObservableProperty] private int _badge;

    public RemoteNotificationsViewModel()
    {
        HasNotificationsEnabled = NotificarePush.HasRemoteNotificationsEnabled && NotificarePush.AllowedUI;
        Badge = NotificareInbox.Badge;

        NotificarePush.NotificationSettingsChanged += OnNotificationsSettingsChanged;
        NotificareInbox.BadgeUpdated += OnBadgeChanged;
    }

    #region Setup Listeners

    private void OnNotificationsSettingsChanged(object? sender, NotificareNotificationSettingsChangedEventArgs e)
    {
        HasNotificationsEnabled = NotificarePush.HasRemoteNotificationsEnabled && NotificarePush.AllowedUI;
    }

    private void OnBadgeChanged(object? sender, NotificareBadgeUpdatedEventArgs e)
    {
        Badge = e.Badge;
    }

    #endregion

    #region Remote Notifications

    public async void EnableRemoteNotifications()
    {
        try
        {
            await NotificarePush.EnableRemoteNotificationsAsync();
        }
        catch (Exception e)
        {
            HasNotificationsEnabled = false;
            Console.WriteLine($"Failed to enable remote notifications: {e.Message}");
        }
    }

    public async void DisableRemoteNotifications()
    {
        try
        {
            await NotificarePush.DisableRemoteNotificationsAsync();
        }
        catch (Exception e)
        {
            HasNotificationsEnabled = true;
            Console.WriteLine($"Failed to disable remote notifications: {e.Message}");
        }
    }

    #endregion
}
