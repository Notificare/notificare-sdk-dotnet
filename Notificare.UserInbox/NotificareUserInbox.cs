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


    /// <summary>
    /// Parses a JSON string to produce a <see cref="NotificareUserInboxResponse"/>.
    ///
    /// This method takes a raw JSON string and converts it into a structured <see cref="NotificareUserInboxResponse"/>.
    /// </summary>
    /// <param name="json">The JSON string representing the user inbox response.</param>
    /// <returns>
    /// A promise that resolves to a <see cref="NotificareUserInboxResponse"/> object parsed from the provided JSON string.
    /// </returns>
    public static Task<NotificareUserInboxResponse> ParseResponseAsync(string json) => Platform.ParseResponseAsync(json);

    /// <summary>
    /// Parses a <see cref="HttpRequestMessage"/> to produce a <see cref="NotificareUserInboxResponse"/>.
    /// </summary>
    /// <param name="response">The <see cref="HttpRequestMessage"/> representing the user inbox response.</param>
    /// <returns>
    /// A promise that resolves to a <see cref="NotificareUserInboxResponse"/> object parsed from the provided HTTP request message.
    /// </returns>
    public static Task<NotificareUserInboxResponse> ParseResponseAsync(HttpResponseMessage response) =>
        Platform.ParseResponseAsync(response);

    /// <summary>
    /// Opens an inbox item and retrieves its associated notification.
    ///
    /// This function opens the provided <see cref="NotificareUserInboxItem"/> and returns the associated <see cref="NotificareNotification"/>.
    /// This operation marks the item as read.
    /// </summary>
    /// <param name="item">The <see cref="NotificareUserInboxItem"/> to be opened.</param>
    /// <returns>
    /// A task that resolves to the <see cref="NotificareNotification"/> associated with the opened inbox item.
    /// </returns>
    public static Task<NotificareNotification> OpenAsync(NotificareUserInboxItem item) => Platform.OpenAsync(item);

    /// <summary>
    /// Marks an inbox item as read.
    ///
    /// This function updates the status of the provided <see cref="NotificareUserInboxItem"/> to read.
    /// </summary>
    /// <param name="item">The <see cref="NotificareUserInboxItem"/> to mark as read.</param>
    /// <returns>
    /// A task that resolves when the inbox item has been successfully marked as read.
    /// </returns>
    public static Task MarkAsReadAsync(NotificareUserInboxItem item) => Platform.MarkAsReadAsync(item);

    /// <summary>
    /// Removes an inbox item from the user's inbox.
    ///
    /// This function deletes the provided <see cref="NotificareUserInboxItem"/> from the user's inbox.
    /// </summary>
    /// <param name="item"> The <see cref="NotificareUserInboxItem"/> to be removed.</param>
    /// <returns>
    /// A task that resolves when the inbox item has been successfully removed.
    /// </returns>
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
