using NotificareSdk.Android.Internal;
using NotificareSdk.Loyalty.Android.Internal;
using NotificareSdk.Loyalty.Core.Internal;
using NotificareSdk.Loyalty.Core.Models;
using NativeNotificare = NotificareSdk.Loyalty.Android.Binding.NotificareLoyaltyCompat;

namespace NotificareSdk.Loyalty.Android;

public class NotificareLoyaltyPlatformAndroid : INotificareLoyaltyPlatform
{
    public void Initialize()
    {
    }

    public async Task<NotificarePass> FetchPassBySerialAsync(string serial)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.FetchPassBySerial(serial, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var pass = (NotificareSdk.Loyalty.Android.Binding.Models.NotificarePass)result;

        return NativeConverter.FromNativePass(pass);
    }

    public async Task<NotificarePass> FetchPassByBarcodeAsync(string barcode)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.FetchPassBySerial(barcode, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var pass = (NotificareSdk.Loyalty.Android.Binding.Models.NotificarePass)result;

        return NativeConverter.FromNativePass(pass);
    }

    public void Present(NotificarePass pass, Activity activity)
    {
        NativeNotificare.Present(activity, NativeConverter.ToNativePass(pass));
    }
}
