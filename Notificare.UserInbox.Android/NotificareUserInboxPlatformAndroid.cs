using NotificareSdk.Android.Internal;
using NotificareSdk.UserInbox.Core.Internal;
using NotificareSdk.UserInbox.Core.Models;
using NotificareSdk.Core.Models;
using NotificareSdk.UserInbox.Android.Internal;
using NativeNotificare = NotificareSdk.UserInbox.Android.Binding.NotificareUserInboxCompat;

namespace NotificareSdk.UserInbox.Android;

public class NotificareUserInboxPlatformAndroid : INotificareUserInboxPlatform
{
    public void Initialize()
    {
    }

    public Task<NotificareUserInboxResponse> ParseResponse(string json)
    {
        var response = NativeNotificare.ParseResponse(json);
        return Task.FromResult(NativeConverter.FromNativeResponse(response));
    }

    public async Task<NotificareUserInboxResponse> ParseResponse(HttpResponseMessage response)
    {
        var responseStr = await response.Content.ReadAsStringAsync();
        return await ParseResponse(responseStr);
    }

    public async Task<NotificareNotification> OpenAsync(NotificareUserInboxItem item)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Open(NativeConverter.ToNativeInboxItem(item), callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var notification = (NotificareSdk.Android.Binding.Models.NotificareNotification)result;

        return NotificareNativeConverter.FromNativeNotification(notification);
    }

    public async Task MarkAsReadAsync(NotificareUserInboxItem item)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.MarkAsRead(NativeConverter.ToNativeInboxItem(item), callback);

        await callback.Task;
    }

    public async Task RemoveAsync(NotificareUserInboxItem item)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Remove(NativeConverter.ToNativeInboxItem(item), callback);

        await callback.Task;
    }
}
