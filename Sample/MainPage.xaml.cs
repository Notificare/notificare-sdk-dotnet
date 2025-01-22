using NotificareSdk;
using NotificareSdk.Core.Events;
using NotificareSdk.Inbox;
using NotificareSdk.Inbox.Core.Events;
using NotificareSdk.Push;
using NotificareSdk.Push.Core.Events;
using NotificareSdk.Push.UI;

namespace Sample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        Notificare.Ready += OnReady;
        Notificare.Unlaunched += OnUnlaunched;
        Notificare.DeviceRegistered += OnDeviceRegistered;

        NotificarePush.NotificationOpened += OnNotificationOpened;
        NotificarePush.NotificationActionOpened += OnNotificationActionOpened;
        NotificarePush.NotificationSettingsChanged += OnNotificationSettingsChanged;

        NotificareInbox.InboxUpdated += OnInboxUpdated;
        NotificareInbox.BadgeUpdated += OnBadgeUpdated;
    }

    #region Core event handlers

    private void OnReady(object? sender, NotificareReadyEventArgs e)
    {
        Console.WriteLine("---> on ready");

        var application = e.Application;
        Console.WriteLine($"application = {application}");
    }

    private void OnUnlaunched(object? sender, NotificareUnlaunchedEventArgs e)
    {
        Console.WriteLine("---> on unlaunched");
    }

    private void OnDeviceRegistered(object? sender, NotificareDeviceRegisteredEventArgs e)
    {
        Console.WriteLine("---> on device registered");

        var device = e.Device;
        Console.WriteLine($"device = {device}");
    }

    #endregion

    #region Push event handlers

    private void OnNotificationOpened(object? sender, NotificareNotificationOpenedEventArgs e)
    {
        Console.WriteLine("---> on notification opened");

        var notification = e.Notification;
        Console.WriteLine($"notification = {notification}");

#if ANDROID
        NotificarePushUI.PresentNotification(notification, Platform.CurrentActivity!);
#elif IOS
        NotificarePushUI.PresentNotification(notification, Platform.GetCurrentUIViewController()!);
#endif
    }

    private void OnNotificationActionOpened(object? sender, NotificareNotificationActionOpenedEventArgs e)
    {
        Console.WriteLine("---> on notification action opened");

        var notification = e.Notification;
        Console.WriteLine($"notification = {notification}");

        var action = e.Action;
        Console.WriteLine($"action = {action}");

#if ANDROID
        NotificarePushUI.PresentAction(notification, action, Platform.CurrentActivity!);
#elif IOS
        NotificarePushUI.PresentAction(notification, action, Platform.GetCurrentUIViewController()!);
#endif
    }

    private void OnNotificationSettingsChanged(object? sender, NotificareNotificationSettingsChangedEventArgs e)
    {
        Console.WriteLine("---> notification settings changed");

        var allowedUI = e.AllowedUI;
        Console.WriteLine($"allowed ui: {allowedUI}");
    }

    #endregion

    #region Inbox event handlers

    private void OnBadgeUpdated(object? sender, NotificareBadgeUpdatedEventArgs e)
    {
        Console.WriteLine("---> badge updated");

        var badge = e.Badge;
        Console.WriteLine($"badge = {badge}");
    }

    private void OnInboxUpdated(object? sender, NotificareInboxUpdatedEventArgs e)
    {
        Console.WriteLine("---> inbox updated");

        var items = e.Items;
        Console.WriteLine($"items = {items}");
    }

    #endregion

    private async void OnEnableRemoteNotificationsClicked(object sender, EventArgs e)
    {
        try
        {
            // TODO: request permission before enabling remote notifications
            
            await NotificarePush.EnableRemoteNotificationsAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"ooooops: {exception}");
        }
    }

    private async void OnDisableRemoteNotificationsClicked(object sender, EventArgs e)
    {
        try
        {
            await NotificarePush.DisableRemoteNotificationsAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"ooooops: {exception}");
        }
    }
}
