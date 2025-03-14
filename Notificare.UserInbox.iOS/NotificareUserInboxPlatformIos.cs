using NotificareSdk.Core.Models;
using NotificareSdk.UserInbox.Core.Internal;
using NotificareSdk.UserInbox.Core.Models;
using NotificareSdk.UserInbox.iOS.Internal;
using NotificareSdk.iOS.Internal;

namespace NotificareSdk.UserInbox.iOS;

public class NotificareUserInboxPlatformIos : INotificareUserInboxPlatform
{
    private Binding.NotificareUserInboxNativeBinding _native = new();

    public void Initialize()
    {
    }

    public Task<NotificareUserInboxResponse> ParseResponseAsync(string json)
    {
        TaskCompletionSource<NotificareUserInboxResponse> completion = new();

        _native.ParseResponseFromString(
            json,
            response => completion.TrySetResult(NativeConverter.FromNativeResponse(response)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public async Task<NotificareUserInboxResponse> ParseResponseAsync(HttpResponseMessage response)
    {
        var responseStr = await response.Content.ReadAsStringAsync();
        return await ParseResponseAsync(responseStr);
    }

    public Task<NotificareNotification> OpenAsync(NotificareUserInboxItem item)
    {
        TaskCompletionSource<NotificareNotification> completion = new();

        _native.Open(
            NativeConverter.ToNativeInboxItem(item),
            notification => completion.TrySetResult(NotificareNativeConverter.FromNativeNotification(notification)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task MarkAsReadAsync(NotificareUserInboxItem item)
    {
        TaskCompletionSource completion = new();

        _native.MarkAsRead(
            NativeConverter.ToNativeInboxItem(item),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task RemoveAsync(NotificareUserInboxItem item)
    {
        TaskCompletionSource completion = new();

        _native.Remove(
            NativeConverter.ToNativeInboxItem(item),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }
}
