using NotificareSdk.Core.Models;
using NotificareSdk.Inbox.Core.Events;
using NotificareSdk.Inbox.Core.Internal;
using NotificareSdk.Inbox.Core.Models;
using NotificareSdk.Inbox.iOS.Internal;
using NotificareSdk.iOS.Internal;

namespace NotificareSdk.Inbox.iOS;

public class NotificareInboxPlatformIos : INotificareInboxPlatform
{
    private InternalNotificareInboxDelegate? _delegate;
    private Binding.NotificareInboxNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalNotificareInboxDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<NotificareInboxUpdatedEventArgs>? InboxUpdated;
    public event EventHandler<NotificareBadgeUpdatedEventArgs>? BadgeUpdated;

    public IList<NotificareInboxItem> Items => _native.Items.Select(NativeConverter.FromNativeInboxItem).ToList();

    public int Badge => _native.Badge.ToInt32();

    public void Refresh() => _native.Refresh();

    public Task<NotificareNotification> OpenAsync(NotificareInboxItem item)
    {
        TaskCompletionSource<NotificareNotification> completion = new();

        _native.Open(
            NativeConverter.ToNativeInboxItem(item),
            notification => completion.TrySetResult(NotificareNativeConverter.FromNativeNotification(notification)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task MarkAsReadAsync(NotificareInboxItem item)
    {
        TaskCompletionSource completion = new();

        _native.MarkAsRead(
            NativeConverter.ToNativeInboxItem(item),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task MarkAllAsReadAsync()
    {
        TaskCompletionSource completion = new();

        _native.MarkAllAsRead(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task RemoveAsync(NotificareInboxItem item)
    {
        TaskCompletionSource completion = new();

        _native.Remove(
            NativeConverter.ToNativeInboxItem(item),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task ClearAsync()
    {
        TaskCompletionSource completion = new();

        _native.Clear(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }


    private sealed class InternalNotificareInboxDelegate : Binding.NotificareInboxNativeBindingDelegate
    {
        private readonly NotificareInboxPlatformIos _platform;

        internal InternalNotificareInboxDelegate(NotificareInboxPlatformIos platform)
        {
            _platform = platform;
        }


        public override void DidUpdateInbox(Binding.NotificareInboxNativeBinding notificareInbox,
            Binding.NotificareInboxItem[] items)
        {
            _platform.InboxUpdated?.Invoke(
                _platform,
                new NotificareInboxUpdatedEventArgs(
                    items.Select(NativeConverter.FromNativeInboxItem).ToList()
                )
            );
        }

        public override void DidUpdateBadge(Binding.NotificareInboxNativeBinding notificareInbox, nint badge)
        {
            _platform.BadgeUpdated?.Invoke(
                _platform,
                new NotificareBadgeUpdatedEventArgs(badge.ToInt32()
                )
            );
        }
    }
}
