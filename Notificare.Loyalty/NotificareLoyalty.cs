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


    /// <summary>
    /// Fetches a pass by its serial number.
    /// </summary>
    /// <param name="serial">The serial number of the pass to be fetched.</param>
    /// <returns>
    /// A task that resolves to the fetched <see cref="NotificarePass"/> corresponding to the given serial number.
    /// </returns>
    public static Task<NotificarePass> FetchPassBySerialAsync(string serial) =>
        Platform.FetchPassBySerialAsync(serial);

    /// <summary>
    /// Fetches a pass by its barcode.
    /// </summary>
    /// <param name="barcode">The barcode of the pass to be fetched.</param>
    /// <returns>
    /// A task that resolves to the fetched <see cref="NotificarePass"/> corresponding to the given barcode.
    /// </returns>
    public static Task<NotificarePass> FetchPassByBarcodeAsync(string barcode) =>
        Platform.FetchPassByBarcodeAsync(barcode);

#if ANDROID
    /// <summary>
    /// Presents a pass to the user.
    /// </summary>
    /// <param name="pass">The <see cref="NotificarePass"/> to be presented to the user.</param>
    /// <param name="activity">The <see cref="Activity"/> context from which the pass presentation will be launched.</param>
    public static void Present(NotificarePass pass, Activity activity) => Platform.Present(pass, activity);
#elif IOS
    /// <summary>
    /// Presents a pass to the user.
    /// </summary>
    /// <param name="pass">The <see cref="NotificarePass"/> to be presented to the user.</param>
    /// <param name="controller">The <see cref="UIViewController"/> from which the pass presentation will be launched.</param>
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
