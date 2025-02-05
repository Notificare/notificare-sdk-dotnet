using NotificareSdk.Loyalty.Core.Internal;
using NotificareSdk.Loyalty.Core.Models;

namespace NotificareSdk.Loyalty;

public static class NotificareLoyalty
{
    private static readonly Lazy<INotificareLoyaltyPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificareLoyaltyPlatform Platform
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


    public static Task<NotificarePass> FetchPassBySerialAsync(string serial) =>
        Platform.FetchPassBySerialAsync(serial);

    public static Task<NotificarePass> FetchPassByBarcodeAsync(string barcode) =>
        Platform.FetchPassByBarcodeAsync(barcode);

#if ANDROID
    public static void Present(NotificarePass pass, Activity activity) => Platform.Present(pass, activity);
#elif IOS
    public static void Present(NotificarePass pass, UIViewController controller) => Platform.Present(pass, controller);
#endif


    private static INotificareLoyaltyPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificareLoyaltyPlatformAndroid();
#elif IOS
        return new iOS.NotificareLoyaltyPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}
