using NotificareSdk.Loyalty.Core.Models;

namespace NotificareSdk.Loyalty.Core.Internal;

public interface INotificareLoyaltyPlatform
{
    void Initialize();

    Task<NotificarePass> FetchPassBySerialAsync(string serial);

    Task<NotificarePass> FetchPassByBarcodeAsync(string barcode);

#if ANDROID
    void Present(NotificarePass pass, Activity activity);
#elif IOS
    void Present(NotificarePass pass, UIViewController controller);
#endif
}
