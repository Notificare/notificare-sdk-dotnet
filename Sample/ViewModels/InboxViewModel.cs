using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotificareSdk.Inbox;
using NotificareSdk.Inbox.Core.Events;
using NotificareSdk.Inbox.Core.Models;
using NotificareSdk.Push.UI;

#if IOS
using UIKit;
#endif

namespace Sample.ViewModels;

public partial class InboxViewModel : ObservableObject
{
    [ObservableProperty] private IList<NotificareInboxItem> _items;

    public InboxViewModel()
    {
        Items = NotificareInbox.Items;

        NotificareInbox.InboxUpdated += OnInboxUpdated;
    }

    public void Cleanup()
    {
        NotificareInbox.InboxUpdated -= OnInboxUpdated;
    }

#if ANDROID
    internal void Open(NotificareInboxItem item)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
        try
        {
            var notification = await NotificareInbox.OpenAsync(item);
            var activity = Platform.CurrentActivity;
            NotificarePushUI.PresentNotification(notification, activity!);

            Console.WriteLine("Opened and presented inbox item successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to open inbox item: {e.Message}");
        }
        });
    }

#elif IOS
    internal void Open(NotificareInboxItem item)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            try
            {
                var notification = await NotificareInbox.OpenAsync(item);
                var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                if (rootViewController is null)
                {
                    Console.WriteLine("Cannot present a notification with a null root view controller.");
                    return;
                }

                if (notification.RequiresViewController())
                {
                    var navigationController = new UINavigationController();
                    if (navigationController.View is not null)
                        navigationController.View.BackgroundColor = UIColor.SystemBackground;

                    rootViewController.PresentViewController(
                        navigationController,
                        true,
                        () => NotificarePushUI.PresentNotification(notification, navigationController)
                    );
                }
                else
                {
                    NotificarePushUI.PresentNotification(notification, rootViewController);
                }

                Console.WriteLine("Opened and presented inbox item successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to open inbox item: {e.Message}");
            }
        });
    }
#endif

    internal void MarkAsRead(NotificareInboxItem item)
    {
        Task.Run(async () =>
        {
            try
            {
                await NotificareInbox.MarkAsReadAsync(item);
                Console.WriteLine("Marked as read inbox item successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to mark as read inbox item: {e.Message}");
            }
        });
    }

    internal void Remove(NotificareInboxItem item)
    {
        Task.Run(async () =>
        {
            try
            {
                await NotificareInbox.RemoveAsync(item);
                Console.WriteLine("Removed inbox item successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to remove inbox item: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void MarkAllAsRead()
    {
        Task.Run(async () =>
        {
            try
            {
                await NotificareInbox.MarkAllAsReadAsync();
                Console.WriteLine("Marked as read all inbox items successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to mark as read all inbox items: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void Clear()
    {
        Task.Run(async () =>
        {
            try
            {
                await NotificareInbox.ClearAsync();
                Console.WriteLine("Cleared inbox successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to clear inbox: {e.Message}");
            }
        });
    }

    [RelayCommand]
    private void Refresh()
    {
        NotificareInbox.Refresh();
    }

    private void OnInboxUpdated(object? sender, NotificareInboxUpdatedEventArgs e)
    {
        Items = e.Items;
    }
}
