using NotificareSdk.Core.Models;
using NotificareSdk.UserInbox.Core.Internal;
using NotificareSdk.UserInbox.Core.Models;

namespace NotificareSdk.UserInbox;

public static class NotificareUserInbox
{
    private static readonly Lazy<INotificareUserInboxPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificareUserInboxPlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }

    
    public static Task<NotificareUserInboxResponse> ParseResponse(string json) => Platform.ParseResponse(json);
    
    public static Task<NotificareUserInboxResponse> ParseResponse(HttpResponseMessage response) => Platform.ParseResponse(response);

    public static Task<NotificareNotification> OpenAsync(NotificareUserInboxItem item) => Platform.OpenAsync(item);

    public static Task MarkAsReadAsync(NotificareUserInboxItem item) => Platform.MarkAsReadAsync(item);

    public static Task RemoveAsync(NotificareUserInboxItem item) => Platform.RemoveAsync(item);


    private static INotificareUserInboxPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificareUserInboxPlatformAndroid();
#elif IOS
        return new iOS.NotificareUserInboxPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}
