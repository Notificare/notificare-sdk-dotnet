using NotificareSdk.Android.Internal;
using NotificareSdk.Inbox.Core.Events;
using NotificareSdk.Inbox.Core.Internal;
using NotificareSdk.Inbox.Core.Models;
using AndroidX.Lifecycle;
using NotificareSdk.Core.Models;
using NotificareSdk.Inbox.Android.Internal;
using NativeNotificare = NotificareSdk.Inbox.Android.Binding.NotificareInboxCompat;

namespace NotificareSdk.Inbox.Android;

public class NotificareInboxPlatformAndroid : INotificareInboxPlatform
{
    private IObserver? _itemsObserver;
    private IObserver? _badgeObserver;

    public void Initialize()
    {
        ObserveInboxItems();
        ObserveBadge();
    }

    public event EventHandler<NotificareInboxUpdatedEventArgs>? InboxUpdated;
    public event EventHandler<NotificareBadgeUpdatedEventArgs>? BadgeUpdated;

    public IList<NotificareInboxItem> Items =>
        NativeNotificare.Items
            .ToEnumerable<Binding.Models.NotificareInboxItem>()
            .Cast<Binding.Models.NotificareInboxItem>()
            .Select(NativeConverter.FromNativeInboxItem)
            .ToList();

    public int Badge => NativeNotificare.Badge;

    public void Refresh() => NativeNotificare.Refresh();

    public async Task<NotificareNotification> OpenAsync(NotificareInboxItem item)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Open(NativeConverter.ToNativeInboxItem(item), callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var notification = (NotificareSdk.Android.Binding.Models.NotificareNotification)result;

        return NotificareNativeConverter.FromNativeNotification(notification);
    }

    public async Task MarkAsReadAsync(NotificareInboxItem item)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.MarkAsRead(NativeConverter.ToNativeInboxItem(item), callback);

        await callback.Task;
    }

    public async Task MarkAllAsReadAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.MarkAllAsRead(callback);

        await callback.Task;
    }

    public async Task RemoveAsync(NotificareInboxItem item)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Remove(NativeConverter.ToNativeInboxItem(item), callback);

        await callback.Task;
    }

    public async Task ClearAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Clear(callback);

        await callback.Task;
    }


    private void ObserveInboxItems()
    {
        if (_itemsObserver != null)
        {
            NativeNotificare.ObservableItems.RemoveObserver(_itemsObserver);
        }

        _itemsObserver = new InboxItemsObserver(this);
        NativeNotificare.ObservableItems.ObserveForever(_itemsObserver);
    }

    private void ObserveBadge()
    {
        if (_badgeObserver != null)
        {
            NativeNotificare.ObservableBadge.RemoveObserver(_badgeObserver);
        }

        _badgeObserver = new BadgeObserver(this);
        NativeNotificare.ObservableBadge.ObserveForever(_badgeObserver);
    }


    private class InboxItemsObserver : Java.Lang.Object, IObserver
    {
        private readonly NotificareInboxPlatformAndroid _platform;

        internal InboxItemsObserver(NotificareInboxPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnChanged(Java.Lang.Object? value)
        {
            if (value is not Java.Util.ICollection items) return;

            _platform.InboxUpdated?.Invoke(
                _platform,
                new NotificareInboxUpdatedEventArgs(
                    items: items
                        .ToEnumerable<Binding.Models.NotificareInboxItem>()
                        .Cast<Binding.Models.NotificareInboxItem>()
                        .Select(NativeConverter.FromNativeInboxItem)
                        .ToList()
                )
            );
        }
    }

    private class BadgeObserver : Java.Lang.Object, IObserver
    {
        private readonly NotificareInboxPlatformAndroid _platform;

        internal BadgeObserver(NotificareInboxPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnChanged(Java.Lang.Object? value)
        {
            if (value is not Java.Lang.Integer badge) return;

            _platform.BadgeUpdated?.Invoke(
                _platform,
                new NotificareBadgeUpdatedEventArgs(
                    badge: badge.IntValue()
                )
            );
        }
    }
}
