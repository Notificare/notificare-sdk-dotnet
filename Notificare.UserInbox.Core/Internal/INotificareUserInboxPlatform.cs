using NotificareSdk.Core.Models;
using NotificareSdk.UserInbox.Core.Models;

namespace NotificareSdk.UserInbox.Core.Internal;

public interface INotificareUserInboxPlatform
{
    void Initialize();

    Task<NotificareUserInboxResponse> ParseResponse(string json);
    
    Task<NotificareUserInboxResponse> ParseResponse(HttpResponseMessage response);
    
    Task<NotificareNotification> OpenAsync(NotificareUserInboxItem item);

    Task MarkAsReadAsync(NotificareUserInboxItem item);

    Task RemoveAsync(NotificareUserInboxItem item);
}
