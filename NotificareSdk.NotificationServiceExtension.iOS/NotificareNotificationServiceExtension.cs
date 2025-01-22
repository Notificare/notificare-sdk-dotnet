using UserNotifications;
using NativeNotificare = NotificareSdk.NotificationServiceExtension.iOS.Binding.NotificareNotificationServiceExtensionNativeBinding;

namespace NotificareSdk.NotificationServiceExtension.iOS;

public static class NotificareNotificationServiceExtension
{
    public static Task<UNNotificationContent> HandleNotificationRequest(UNNotificationRequest request)
    {
        TaskCompletionSource<UNNotificationContent> completion = new();

        NativeNotificare.HandleNotificationRequest(
            request,
            content => completion.TrySetResult(content),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }
}
